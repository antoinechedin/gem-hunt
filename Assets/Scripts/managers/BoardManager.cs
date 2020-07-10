using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public Sprite stevenSprite;
    public Sprite peridotSprite;
    public readonly string[] DICE_POOL = { "green", "green", "green", "green", "green", "green", "orange", "orange", "orange", "orange", "red", "red", "red" };

    public Dice[] dices;
    public BoardUI boardUI;
    public GameObject playerTokenPrefab;
    public GameObject pauseMenuPrefab;
    public GameObject rulesMenuPrefab;
    public AudioSource music;
    public BoardSoundsEffect boardSoundsEffect;
    public MenuSoundEffect menuSoundEffect;

    private List<Character> characterList;
    private List<PlayerToken> playerList;
    private Vector2[] playerPositions;
    private int currentPlayer = 0;
    private List<string> colorPool;
    private List<string> colorTrashBin;
    private bool canPlay = false;
    private float maxVolume;
    [HideInInspector] public bool pause = false;
    [HideInInspector] public GameObject pauseMenu;
    [HideInInspector] public GameObject rulesMenu;

    private void Awake()
    {
        characterList = GameManager.instance.GetCharacterList();

        // characterList = new List<Character>();
        // characterList.Add(new Character(0, "Steven", stevenSprite, new Color(1f, 0f, 0f)));
        // characterList.Add(new Character(1, "Peridot", peridotSprite, new Color(0f, 1f, 0f)));

        playerList = new List<PlayerToken>();
        for (int i = 0; i < characterList.Count; i++)
        {
            PlayerToken pt = Instantiate(playerTokenPrefab, new Vector2(600 + i * 220, -540), Quaternion.identity).GetComponent<PlayerToken>();
            pt.transform.SetParent(boardUI.transform, false);
            pt.SetCharacter(characterList[i]);
            playerList.Add(pt);
        }
        playerList.Shuffle();

        playerPositions = new Vector2[5]
            {new Vector2(125f, -125.28f), new Vector2(125f, -332.64f), new Vector2(125f, -540f), new Vector2(125f, -747.36f), new Vector2(125f, -954.72f)};

        boardUI.startButton.GetComponent<Button>().onClick.AddListener(delegate { StartTurnAction(); });
        boardUI.continueButton.GetComponent<Button>().onClick.AddListener(delegate { ContinueAction(); });
        boardUI.stopButton.GetComponent<Button>().onClick.AddListener(delegate { StopAction(); });

        boardUI.resultButton.GetComponent<Button>().onClick.AddListener(Results);
        boardUI.pauseButton.onClick.AddListener(Pause);

        colorPool = new List<string>();
        colorTrashBin = new List<string>();

        music = GetComponent<AudioSource>();
        maxVolume = music.volume;
        music.volume = 0;

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MusicFade(1f, 1.7f, 1));
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return StartCoroutine(GameManager.instance.FadeOut(1f));

        if (!PlayerPrefs.HasKey("gameCount"))
        {
            pause = true;
            RulesMenu rm = Instantiate(rulesMenuPrefab, boardUI.transform).GetComponent<RulesMenu>();
            rm.boardManager = this;
        }

        while(pause) yield return null;
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < playerList.Count; i++)
        {
            StartCoroutine(playerList[i].MoveTo(playerPositions[i], 1, 2));
            StartCoroutine(playerList[i].ShowScore(1f));
        }
        StartCoroutine(boardUI.BackgroudFade(0, 1));
        yield return new WaitForSeconds(1);

        yield return StartCoroutine(InitTurn());
    }

    private IEnumerator InitTurn()
    {
        while (pause)
            yield return null;
        colorPool.Clear();
        colorPool.AddRange(DICE_POOL);
        colorTrashBin.Clear();

        playerList[currentPlayer].SetLife(3);
        StartCoroutine(playerList[currentPlayer].pullOutScore(0.5f));
        StartCoroutine(boardUI.ShowHearts());
        boardUI.ResetMiniDice(null);
        bool isPlayer = playerList[currentPlayer].GetCharacter().type == "player";
        StartCoroutine(boardUI.ShowTurnTitle(playerList[currentPlayer].GetCharacter().name, playerList[currentPlayer].GetCharacter().color, isPlayer));
        yield return new WaitForSeconds(0.7f);

        if (isPlayer)
            canPlay = true;
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartTurnAction(true);
        }
    }

    private IEnumerator RollAction(bool firstRoll, string type)
    {
        while (pause)
            yield return null;

        if (firstRoll)
        {
            StartCoroutine(boardUI.HideTurnTitle(type == "player"));
            if (type == "player")
                StartCoroutine(boardUI.ShowButtons(playerList[currentPlayer].GetCharacter().color));
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            int diceToDraw = 0;
            List<string> colorList = new List<string>();
            foreach (Dice dice in dices)
            {
                if (dice.GetFaceIndex() != 1)
                {
                    diceToDraw++;
                    colorTrashBin.Add(dice.GetColor());

                    StartCoroutine(dice.Shrink());
                }
                else if (!firstRoll)
                {
                    colorList.Add(dice.GetColor());
                }
            }
            if (diceToDraw > 0)
            {
                if (diceToDraw > colorPool.Count)
                {
                    Dictionary<string, int> colorCount = new Dictionary<string, int>();
                    foreach (string color in colorList)
                    {
                        if (colorCount.ContainsKey(color))
                            colorCount[color]++;
                        else
                            colorCount.Add(color, 1);
                    }
                    boardUI.ResetMiniDice(colorCount);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        foreach (Dice dice in dices)
        {
            if (dice.GetFaceIndex() != 1 || firstRoll)
            {
                if (colorPool.Count == 0)
                {
                    colorPool.AddRange(colorTrashBin);
                    colorTrashBin.Clear();
                }
                int index = Random.Range(0, colorPool.Count);
                dice.SetColor(colorPool[index]);
                StartCoroutine(boardUI.MiniDiceShrink(colorPool[index]));
                colorPool.RemoveAt(index);
            }
        }
        Debug.LogFormat(this, "Color pool[{0}] : {1}", colorPool.Count, string.Join(", ", colorPool.ToArray()));

        while (pause)
            yield return null;

        boardSoundsEffect.audioSource.PlayOneShot(boardSoundsEffect.dice);
        foreach (Dice dice in dices)
        {
            bool grow = firstRoll ? true : dice.GetFaceIndex() != 1;
            StartCoroutine(dice.Roll(grow, type));
            playerList[currentPlayer].GetCharacter().gameStat.AddDiceFace(dice.GetFaceIndex());
        }

        yield return new WaitForSeconds(0.8f);

        while (pause)
            yield return null;

        List<Dice> clawList = new List<Dice>();
        foreach (Dice dice in dices)
        {
            if (dice.GetFaceIndex() == 2)
            {
                //dice.animator.SetTrigger("highlight");
                if (playerList[currentPlayer].GetLife() > 0)
                    StartCoroutine(boardUI.LoseLife(1));
                playerList[currentPlayer].SetLife(playerList[currentPlayer].GetLife() - 1);
                clawList.Add(dice);
                boardSoundsEffect.audioSource.PlayOneShot(boardSoundsEffect.negative);
                StartCoroutine(dice.Highlight());
                yield return new WaitForSeconds(0.2f);
            }
        }
        if (clawList.Count > 0)
        {
            yield return new WaitForSeconds(0.2f);
            foreach (Dice dice in clawList)
                StartCoroutine(dice.Lowlight());
            yield return new WaitForSeconds(0.3f);
        }

        while (pause)
            yield return null;

        if (playerList[currentPlayer].GetLife() <= 0)
            StartCoroutine(TurnOver());
        else
        {
            List<Dice> bubbleList = new List<Dice>();
            foreach (Dice dice in dices)
            {
                Coroutine gainTurnScore = null;
                if (dice.GetFaceIndex() == 0)
                {
                    //dice.animator.SetTrigger("highlight");
                    bubbleList.Add(dice);
                    boardSoundsEffect.audioSource.PlayOneShot(boardSoundsEffect.positive);
                    StartCoroutine(dice.Highlight());
                    if (gainTurnScore != null)
                        StopCoroutine(gainTurnScore);
                    gainTurnScore = StartCoroutine(playerList[currentPlayer].GainTurnScore(1));
                    yield return new WaitForSeconds(0.2f);
                }
            }
            if (bubbleList.Count > 0)
            {
                yield return new WaitForSeconds(0.2f);
                foreach (Dice dice in bubbleList)
                    StartCoroutine(dice.Lowlight());
                yield return new WaitForSeconds(0.3f);
            }

            while (pause)
                yield return null;

            if (type == "player")
                canPlay = true;
            else
            {
                yield return new WaitForSeconds(0.2f);
                if (playerList[currentPlayer].GetScore() + playerList[currentPlayer].GetTurnScore() >= 13)
                    StopAction(true);
                else if (playerList[currentPlayer].GetTurnScore() == 0 || playerList[currentPlayer].GetLife() >= 3)
                    ContinueAction(true);
                else if (playerList[currentPlayer].GetLife() == 1)
                    StopAction(true);
                else if (playerList[currentPlayer].GetTurnScore() < 3)
                    ContinueAction(true);
                else if (playerList[currentPlayer].GetTurnScore() >= 2 && playerList[currentPlayer].GetTurnScore() <= 4)
                {
                    if ((Random.Range(0, 2) == 0))
                        ContinueAction(true);
                    else
                        StopAction(true);
                }
                else
                    StopAction(true);
            }
        }
    }

    private void StartTurnAction(bool force = false)
    {
        if (canPlay || force)
        {
            canPlay = false;
            StartCoroutine(RollAction(true, playerList[currentPlayer].GetCharacter().type));
        }
    }

    private void ContinueAction(bool force = false)
    {
        if (canPlay || force)
        {
            canPlay = false;
            StartCoroutine(RollAction(false, playerList[currentPlayer].GetCharacter().type));
        }
    }

    private void StopAction(bool force = false)
    {
        if (canPlay || force)
        {
            canPlay = false;
            StartCoroutine(NextTurn());
        }
    }

    private IEnumerator NextTurn()
    {
        yield return StartCoroutine(playerList[currentPlayer].pullInScore(0.5f, boardSoundsEffect));
        EndTurn();

        while (pause)
            yield return null;

        if (playerList[currentPlayer].GetScore() >= 13)
        {
            List<Character> characterList = new List<Character>();
            for (int i = 0; i < playerList.Count; i++)
            {
                Character c = playerList[i].GetCharacter();
                c.gameStat.score = playerList[i].GetScore();
                characterList.Add(c);
            }
            GameManager.instance.SetCharacterList(characterList);

            yield return new WaitForSeconds(0.4f);
            boardSoundsEffect.audioSource.PlayOneShot(boardSoundsEffect.win);
            StartCoroutine(boardUI.ShowWin(playerList[currentPlayer]));
            yield return new WaitForSeconds(0.4f);

            if (PlayerPrefs.HasKey("gameCount"))
                PlayerPrefs.SetInt("gameCount", PlayerPrefs.GetInt("gameCount") + 1);
            else
                PlayerPrefs.SetInt("gameCount", 1);
            PlayerPrefs.Save();

            canPlay = true;
        }
        else
        {
            yield return StartCoroutine(NextPlayer());
            yield return StartCoroutine(InitTurn());
        }
    }

    private IEnumerator TurnOver()
    {
        yield return new WaitForSeconds(0.2f);
        boardSoundsEffect.audioSource.PlayOneShot(boardSoundsEffect.lose);
        yield return playerList[currentPlayer].loseScore(0.5f);

        EndTurn();
        yield return StartCoroutine(NextPlayer());
        yield return StartCoroutine(InitTurn());
    }

    private void EndTurn()
    {
        boardUI.MiniDiceShrinkAll();
        StartCoroutine(boardUI.HideButtons());
        StartCoroutine(boardUI.HideHearts());
        foreach (Dice dice in dices)
            StartCoroutine(dice.Shrink());
    }

    private IEnumerator NextPlayer()
    {
        float duration = 0.7f;
        float t = 0;

        StartCoroutine(playerList[currentPlayer].MoveTo(boardUI.outPlayer, duration, 2));
        yield return new WaitForSeconds(0.1f);
        t += 0.1f;
        for (int i = 1; i < playerList.Count; i++)
        {
            int index = currentPlayer + i < playerList.Count ? currentPlayer + i : currentPlayer + i - playerList.Count;
            StartCoroutine(playerList[index].MoveTo(boardUI.playerPositions[i - 1], duration, 2));
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }

        if (t < duration)
            yield return new WaitForSeconds(duration - t);
        playerList[currentPlayer].rectTransform.anchoredPosition = boardUI.inPlayer;
        StartCoroutine(playerList[currentPlayer].MoveTo(boardUI.playerPositions[playerList.Count - 1], duration, 1));
        yield return new WaitForSeconds(duration);

        currentPlayer++;
        if (currentPlayer == playerList.Count)
            currentPlayer = 0;
    }

    private void Pause()
    {
        pauseMenu = Instantiate(pauseMenuPrefab, boardUI.transform);
        pauseMenu.GetComponent<PauseMenu>().SetBoardManager(this);
        pause = true;
    }

    private void Results()
    {
        if (canPlay)
        {
            canPlay = false;
            StartCoroutine(MusicFade(-1f, 1f, 1));
            StartCoroutine(GameManager.instance.LoadScene("Results"));
        }
    }

    public IEnumerator MusicFade(float direction, float duration, int type)
    {
        float t = 0;
        float start = music.volume;

        if (direction == 1)
            music.Play();


        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float x = 0;
            if (type == 0) //In
                x = Easing.EaseInQuad(t, 0, 1, duration) * direction;
            else if (type == 1) //Out
                x = Easing.EaseOutQuad(t, 0, 1, duration) * direction;
            else if (type == 2) //InOut
                x = Easing.EaseInOutQuad(t, 0, 1, duration) * direction;
            else if (type == 3)
                x = Easing.EaseLinear(t, 0, 1, duration) * direction;

            music.volume = start + x * maxVolume;
            yield return null;
        }

        if (direction == -1)
            music.Stop();
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.fading)
        {
            if(pause)
            {
                if(pauseMenu != null)
                    Destroy(pauseMenu.gameObject);
                if(rulesMenu != null)
                    Destroy(rulesMenu.gameObject);
                pause = false;
            }
            else
            {
                Pause();
            }
        }
    }
}


static class ShuffleExtension
{
    public static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

