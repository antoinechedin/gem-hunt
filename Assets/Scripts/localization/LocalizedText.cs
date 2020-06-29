using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    public TextMeshPro[] texts;
    public TextMeshProUGUI[] uiTexts;

    private Alphabet alphabet = Alphabet.Latin;

    private void Start()
    {
        SetLocalizedText();
    }

    private void OnEnable()
    { LocalizationManager.OnUpdateText += SetLocalizedText; }

    private void OnDisable()
    { LocalizationManager.OnUpdateText -= SetLocalizedText; }

    public void SetText(string text)
    {
        UpdateAlphbet();

        foreach (TextMeshPro t in texts)
        { if (t != null) t.text = text; }
        foreach (TextMeshProUGUI t in uiTexts)
        { if (t != null) t.text = text; }
    }

    public void SetLocalizedText()
    {
        UpdateAlphbet();

        foreach (TextMeshPro t in texts)
        { if (t != null) t.text = LocalizationManager.instance.GetLocalizedValue(key); }
        foreach (TextMeshProUGUI t in uiTexts)
        { if (t != null) t.text = LocalizationManager.instance.GetLocalizedValue(key); }
    }
    public void SetLocalizedFormatedText(string[] args)
    {
        UpdateAlphbet();

        foreach (TextMeshPro t in texts)
        { if (t != null) t.text = string.Format(LocalizationManager.instance.GetLocalizedValue(key), args); }
        foreach (TextMeshProUGUI t in uiTexts)
        { if (t != null) t.text = string.Format(LocalizationManager.instance.GetLocalizedValue(key), args); }
    }

    private void UpdateAlphbet()
    {
        if (alphabet != LocalizationManager.instance.Alphabet)
        {
            alphabet = LocalizationManager.instance.Alphabet;
            if (alphabet == Alphabet.Latin)
            {
                if (texts.Length > 0 && texts[0] != null) texts[0].font = LocalizationManager.instance.latin;
                if (texts.Length > 1 && texts[1] != null) texts[1].font = LocalizationManager.instance.latinOutline;
                if (texts.Length > 2 && texts[2] != null) texts[2].font = LocalizationManager.instance.latinShadow;

                if (uiTexts.Length > 0 && uiTexts[0] != null) uiTexts[0].font = LocalizationManager.instance.latin;
                if (uiTexts.Length > 1 && uiTexts[1] != null) uiTexts[1].font = LocalizationManager.instance.latinOutline;
                if (uiTexts.Length > 2 && uiTexts[2] != null) uiTexts[2].font = LocalizationManager.instance.latinShadow;
            }
            else if (alphabet == Alphabet.Cyrillic)
            {
                if (texts.Length > 0 && texts[0] != null) texts[0].font = LocalizationManager.instance.cyrillic;
                if (texts.Length > 1 && texts[1] != null) texts[1].font = LocalizationManager.instance.cyrillicOutline;
                if (texts.Length > 2 && texts[2] != null) texts[2].font = LocalizationManager.instance.cyrillicShadow;

                if (uiTexts.Length > 0 && uiTexts[0] != null) uiTexts[0].font = LocalizationManager.instance.cyrillic;
                if (uiTexts.Length > 1 && uiTexts[1] != null) uiTexts[1].font = LocalizationManager.instance.cyrillicOutline;
                if (uiTexts.Length > 2 && uiTexts[2] != null) uiTexts[2].font = LocalizationManager.instance.cyrillicShadow;
            }
        }
    }

}
