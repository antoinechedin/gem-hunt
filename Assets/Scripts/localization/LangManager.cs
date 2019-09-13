using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangManager : MonoBehaviour
{
    public Button french;
    public Button english;
    public AudioSource audioSource;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("lang"))
        {
            if (french != null)
                french.onClick.AddListener(delegate { SetLang("french"); });
            if (english != null)
                english.onClick.AddListener(delegate { SetLang("english"); });
        }
    }

    private void SetLang(string lang)
    {
        audioSource.Play();
        PlayerPrefs.SetString("lang", lang);
        PlayerPrefs.Save();
        LocalizationManager.instance.LoadLocalizedText(lang);
        french.interactable = false;
        english.interactable = false;
        StartCoroutine(GameManager.instance.LoadScene("Lobby", true));
    }

}
