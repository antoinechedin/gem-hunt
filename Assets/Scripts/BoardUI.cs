using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;

public class BoardUI : MonoBehaviour
{
    public Image uiBackground;
    public GameObject[] miniGreenDices;
    public GameObject[] miniOrangeDices;
    public GameObject[] miniRedDices;
    public Vector2[] playerPositions;
    public GameObject[] hearts;

    public GameObject turnTitle;
    public LocalizedText turnText;
    public SUButton startButton;

    public GameObject buttonsParent;
    public SUButton stopButton;
    public SUButton continueButton;

    public GameObject winTitle;
    public LocalizedText winText;
    public Button pauseButton;
    public SUButton resultButton;

    [HideInInspector] public Dictionary<string, GameObject[]> miniDiceDictionary;
    [HideInInspector] public Vector2 outPlayer;
    [HideInInspector] public Vector2 inPlayer;

    private int life;
    private Dictionary<string, int> miniDiceNumberDictionary;

    private Image winCharacter;

    private void Awake()
    {
        // BoardManager.instance.ui = this;

        miniDiceDictionary = new Dictionary<string, GameObject[]>();
        miniDiceDictionary.Add("green", miniGreenDices);
        miniDiceDictionary.Add("orange", miniOrangeDices);
        miniDiceDictionary.Add("red", miniRedDices);

        miniDiceNumberDictionary = new Dictionary<string, int>();
        miniDiceNumberDictionary.Add("green", 0);
        miniDiceNumberDictionary.Add("orange", 0);
        miniDiceNumberDictionary.Add("red", 0);
        foreach (GameObject md in miniGreenDices)
            md.GetComponent<RectTransform>().localScale = new Vector2(0f, 0f);
        foreach (GameObject md in miniOrangeDices)
            md.GetComponent<RectTransform>().localScale = new Vector2(0f, 0f);
        foreach (GameObject md in miniRedDices)
            md.GetComponent<RectTransform>().localScale = new Vector2(0f, 0f);

        uiBackground = transform.GetChild(0).GetComponent<Image>();
        outPlayer = new Vector2(125, 125.28f);
        inPlayer = new Vector2(125, -1205.28f);

        winCharacter = winTitle.transform.GetChild(0).GetComponent<Image>();
    }

    public IEnumerator BackgroudFade(float alpha, float duration)
    {
        float start = uiBackground.color.a;
        Color c = uiBackground.color;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float val = Easing.EaseInOutCubic(t, start, alpha - start, duration);
            uiBackground.color = new Color(c.r, c.g, c.b, val);

            yield return null;
        }
    }

    public IEnumerator ShowHearts()
    {
        life = 3;
        for (int i = 0; i < 3; i++)
        {
            hearts[i].GetComponent<Animator>().SetTrigger("grow");
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator HideHearts()
    {
        for (int i = 0; i < life; i++)
        {
            hearts[i].GetComponent<Animator>().SetTrigger("hide");
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator LoseLife(int amount)
    {
        amount = amount > life ? life : amount;
        for (int i = 0; i < amount; i++)
        {
            hearts[life - i - 1].GetComponent<Animator>().SetTrigger("hit");
        }
        life -= amount;
        yield return new WaitForSeconds(0.2f);
    }

    public void ResetMiniDice(Dictionary<string, int> colorCount)
    {
        foreach (string color in miniDiceDictionary.Keys)
        {
            int offset = 0;
            if (colorCount != null && colorCount.ContainsKey(color))
                offset = colorCount[color];
            for (int i = miniDiceNumberDictionary[color]; i < miniDiceDictionary[color].Length - offset; i++)
            {
                StartCoroutine(MiniDiceGrow(color, i));
            }
            miniDiceNumberDictionary[color] = miniDiceDictionary[color].Length - offset;
        }
    }

    public IEnumerator MiniDiceGrow(string color, int index)
    {

        float duration = 0.4f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            miniDiceDictionary[color][index].GetComponent<RectTransform>().localScale = new Vector2(x, x);
            yield return null;
        }
    }

    public IEnumerator MiniDiceShrink(string color)
    {
        miniDiceNumberDictionary[color]--;
        int diceIndex = miniDiceNumberDictionary[color];

        float duration = 0.5f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            miniDiceDictionary[color][diceIndex].GetComponent<RectTransform>().localScale = new Vector2(1 - x, 1 - x);
            yield return null;
        }
    }

    public void MiniDiceShrinkAll()
    {
        foreach (string color in miniDiceDictionary.Keys)
        {
            while (miniDiceNumberDictionary[color] > 0)
            {
                StartCoroutine(MiniDiceShrink(color));
            }
            miniDiceNumberDictionary[color] = 0;
        }
    }

    public IEnumerator ShowTurnTitle(string characterName, Color characterColor, bool showButton)
    {
        bool alt = LocalizationManager.instance.Alphabet == Alphabet.Cyrillic;

        turnText.SetLocalizedFormatedText(new string[1] {LocalizationManager.instance.GetLocalizedValue(characterName, alt)});
        turnText.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = characterColor;
        startButton.SetTextColor(characterColor);

        float duration = 0.4f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            turnTitle.GetComponent<RectTransform>().localScale = new Vector2(x, x);
            if (showButton)
                startButton.GetComponent<RectTransform>().localScale = new Vector2(x, x);
            yield return null;
        }
    }

    public IEnumerator HideTurnTitle(bool hideButton)
    {
        float duration = 0.3f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            turnTitle.GetComponent<RectTransform>().localScale = new Vector2(1 - x, 1 - x);
            if (hideButton)
                startButton.GetComponent<RectTransform>().localScale = new Vector2(1 - x, 1 - x);
            yield return null;
        }
    }

    public IEnumerator ShowButtons(Color characterColor)
    {
        buttonsParent.SetActive(true);

        continueButton.SetTextColor(characterColor);
        stopButton.SetTextColor(characterColor);

        float duration = 0.4f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            buttonsParent.GetComponent<RectTransform>().localScale = new Vector2(x, x);
            yield return null;
        }
    }

    public IEnumerator HideButtons()
    {
        float duration = 0.3f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            buttonsParent.GetComponent<RectTransform>().localScale = new Vector2(1 - x, 1 - x);
            yield return null;
        }
        buttonsParent.SetActive(false);
    }

    public IEnumerator ShowWin(PlayerToken player)
    {
        StartCoroutine(BackgroudFade(0.6f, 0.4f));

        winTitle.SetActive(true);

        winCharacter.sprite = player.image.sprite;
        resultButton.SetTextColor(player.GetCharacter().color);
        winText.SetLocalizedFormatedText(new string[1] {LocalizationManager.instance.GetLocalizedValue(player.GetCharacter().name)});
        winText.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = player.GetCharacter().color;

        float duration = 0.4f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, 1, duration);
            winTitle.GetComponent<RectTransform>().localScale = new Vector2(x, x);
            yield return null;
        }
    }
}
