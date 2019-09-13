using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    public TextMeshPro[] texts;
    public TextMeshProUGUI[] uiTexts;

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
        foreach (TextMeshPro t in texts)
        { t.text = text; }
        foreach (TextMeshProUGUI t in uiTexts)
        { t.text = text; }
    }

    public void SetLocalizedText()
    {
        foreach (TextMeshPro t in texts)
        { t.text = LocalizationManager.instance.GetLocalizedValue(key); }
        foreach (TextMeshProUGUI t in uiTexts)
        { t.text = LocalizationManager.instance.GetLocalizedValue(key); }
    }
    public void SetLocalizedFormatedText(string[] args)
    {
        foreach (TextMeshPro t in texts)
        { t.text = string.Format(LocalizationManager.instance.GetLocalizedValue(key), args); }
        foreach (TextMeshProUGUI t in uiTexts)
        { t.text = string.Format(LocalizationManager.instance.GetLocalizedValue(key), args); }
    }
}
