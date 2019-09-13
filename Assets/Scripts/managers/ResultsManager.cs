using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ResultsManager : MonoBehaviour
{
    public CharacterResult[] results;
    public Button homeButton;
    public Button restartButton;
    public AudioSource music;

    private List<Character> characterSortedList;
    private bool canClick = false;
    private float maxVolume;

    private void Awake()
    {
        characterSortedList = new List<Character>();
        List<Character> characters = GameManager.instance.GetCharacterList();

        while (characters.Count > 0)
        {
            int maxScore = -1;
            Character character = null;
            foreach (Character c in characters)
                if (c.gameStat.score > maxScore)
                {
                    maxScore = c.gameStat.score;
                    character = c;
                }
            characterSortedList.Add(character);
            characters.Remove(character);
        }
        homeButton.onClick.AddListener(Home);
        restartButton.onClick.AddListener(Restart);

        maxVolume = music.volume;
    }

    void Start()
    {
        StartCoroutine(MusicFade(1f, 1.7f, 1));
        int actualScore = int.MaxValue;
        int offset = 0;
        for (int i = 0; i < 5; i++)
        {
            if (i < characterSortedList.Count)
            {
                if (characterSortedList[i].gameStat.score < actualScore)
                {
                    actualScore = characterSortedList[i].gameStat.score;
                    offset = 0;
                }
                else
                    offset++;

                results[i].SetCharacter(characterSortedList[i], 1 + i - offset);
            }
            else
                results[i].SetCharacter(null);
        }
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return StartCoroutine(GameManager.instance.FadeOut(1f));
        canClick = true;
        yield return null;
    }

    public void Restart()
    {
        if (canClick)
        {
            canClick = false;
            foreach (Character c in characterSortedList)
            {
                c.gameStat = new GameStat();
            }
            GameManager.instance.SetCharacterList(characterSortedList);
            StartCoroutine(MusicFade(-1f, 1f, 1));
            StartCoroutine(GameManager.instance.LoadScene("Board"));
        }
    }

    public void Home()
    {
        if (canClick)
        {
            canClick = false;
            GameManager.instance.SetCharacterList(null);
            StartCoroutine(MusicFade(-1f, 1f, 1));
            StartCoroutine(GameManager.instance.LoadScene("Lobby"));
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
            Home();
        }
    }
}
