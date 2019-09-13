using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class LobbyManager : MonoBehaviour
{
    public MovingCamera cam;
    public Button playerPlus;
    public Button playerMinus;
    public CharacterSelector[] playerSelectors;
    public Button computerPlus;
    public Button computerMinus;
    public GameObject[] computers;
    public Sprite computerDefaultSprite;
    public Character[] computerCharacters;
    public TextOutline counterText;
    public SelectionScreen selectionScreen;
    public Button startButton;
    public AudioSource music;

    private int playerCount = 2;
    private int computerCount = 0;
    private int? selectorIndex = null;
    private Vector2 startPosition;
    private bool canChooseChar = false;
    private float maxVolume;

    private void Awake()
    {
        selectionScreen.gameObject.SetActive(true);

        playerPlus.onClick.AddListener(AddOnePlayer);
        playerMinus.onClick.AddListener(RemoveOnePlayer);
        computerPlus.onClick.AddListener(AddOneComputer);
        computerMinus.onClick.AddListener(RemoveOneComputer);

        for (int i = 0; i < playerSelectors.Length; i++)
        {
            CharacterSelector cs = playerSelectors[i];
            cs.SetButtonEnable(i < playerCount);
            cs.SetIndex(i);
            cs.button.onClick.AddListener(delegate { StartCoroutine(StartSelectCharacterFor(cs.GetIndex())); });
        }

        foreach (CharacterButton cb in selectionScreen.characters)
        {
            cb.button.onClick.AddListener(delegate { StartCoroutine(SelectCharacterAt(cb.character.id)); });
        }

        foreach (Character c in computerCharacters)
            c.type = "computer";

        startButton.onClick.AddListener(StartGame);
        startPosition = new Vector2(0f, 0f);

        music = GetComponent<AudioSource>();
        maxVolume = music.volume;
        music.volume = 0;

        UpdateCounterText();
    }

    private void Start()
    {
        StartCoroutine(MusicFade(1f, 0.5f, 1));
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return StartCoroutine(GameManager.instance.FadeOut(1f));
    }

    public void AddOnePlayer()
    {
        if (playerCount < 5)
        {
            playerSelectors[playerCount].SetButtonEnable(true);
            playerCount++;

            UpdateCounterText();
        }
    }

    public void RemoveOnePlayer()
    {
        if (playerCount > 0)
        {
            playerCount--;
            if (playerSelectors[playerCount].GetCharacter() != null)
                selectionScreen.characters[playerSelectors[playerCount].GetCharacter().id].button.interactable = true;
            playerSelectors[playerCount].SetButtonEnable(false);

            UpdateCounterText();
        }
    }

    public void AddOneComputer()
    {
        if (computerCount < 4)
        {
            computers[computerCount].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            computers[computerCount].GetComponent<Image>().sprite = computerCharacters[computerCount].sprite;
            computerCount++;

            UpdateCounterText();
        }
    }

    public void RemoveOneComputer()
    {
        if (computerCount > 0)
        {
            computerCount--;
            computers[computerCount].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
            computers[computerCount].GetComponent<Image>().sprite = computerDefaultSprite;

            UpdateCounterText();
        }
    }

    public void UpdateCounterText()
    {
        string s = "{0}/5 max";
        int totalPlayer = playerCount + computerCount;
        counterText.SetText(string.Format(s, playerCount + computerCount));
        if (totalPlayer >= 2 && totalPlayer <= 5)
            counterText.SetColor(new Color(1f, 1f, 1f));
        else
            counterText.SetColor(new Color32(244, 67, 54, 255));
    }

    public IEnumerator StartSelectCharacterFor(int index)
    {
        selectorIndex = index;
        if (playerSelectors[index].GetCharacter() != null)
            selectionScreen.characters[playerSelectors[index].GetCharacter().id].button.interactable = true;
        StartCoroutine(selectionScreen.FadeInBackground());
        yield return StartCoroutine(selectionScreen.ShowCharacterButtons());
        canChooseChar = true;
    }

    public IEnumerator SelectCharacterAt(int id)
    {
        if (canChooseChar)
        {
            canChooseChar = false;
            if (selectorIndex != null && id >= 0)
            {
                CharacterButton cb = selectionScreen.characters[id];
                cb.button.interactable = false;
                playerSelectors[selectorIndex.Value].SetCharacter(cb.character);
                selectorIndex = null;
            }
            StartCoroutine(selectionScreen.FadeOutBackground());
            yield return StartCoroutine(selectionScreen.HideCharacterButtons());
            selectorIndex = null;
        }
    }

    public void StartGame()
    {
        // Check maximun number of players
        if (playerCount + computerCount > 5 || playerCount + computerCount < 2)
            StartCoroutine(counterText.Flash());
        else // Check if all player have selected a character
        {
            bool ok = true;
            List<Character> characterList = new List<Character>();
            bool sound = true;
            foreach (CharacterSelector cs in playerSelectors)
            {
                if (cs.IsButtonEnable())
                {
                    if (cs.GetCharacter() != null)
                        characterList.Add(cs.GetCharacter());
                    else
                    {
                        ok = false;
                        StartCoroutine(cs.Flash(sound));
                        sound = false;
                    }
                }
            }
            if (ok)
            {
                for (int i = 0; i < computerCount; i++)
                {
                    characterList.Add(computerCharacters[i]);
                }
                StartCoroutine(MusicFade(-1, 1f, 1));
                GameManager.instance.SetCharacterList(characterList);
                StartCoroutine(GameManager.instance.LoadScene("Board"));
            }
        }
    }

    private Vector2 GetWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
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
            if (cam.roomIndex == 0)
                StartCoroutine(cam.Move(1));
            else if (cam.roomIndex == 1)
                Application.Quit();
            else if (cam.roomIndex == 2)
                if (selectorIndex == null)
                    StartCoroutine(cam.Move(-1));
                else if (canChooseChar)
                    StartCoroutine(SelectCharacterAt(-1));
        }
    }
}
