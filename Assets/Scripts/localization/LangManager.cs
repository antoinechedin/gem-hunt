using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangManager : MonoBehaviour
{
    public Button french;
    public Button english;
    public Button spanish;
    public Button portuguese;
    public Button russian;
    public AudioSource audioSource;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("lang"))
        {
            if (french != null)
                french.onClick.AddListener(delegate { SetLang("french"); });
            if (english != null)
                english.onClick.AddListener(delegate { SetLang("english"); });
            if (spanish != null)
                spanish.onClick.AddListener(delegate { SetLang("spanish"); });
            if (portuguese != null)
                portuguese.onClick.AddListener(delegate { SetLang("portuguese"); });
            if (russian != null)
                russian.onClick.AddListener(delegate { SetLang("russian"); });
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
        portuguese.interactable = false;
        StartCoroutine(GameManager.instance.LoadScene("Lobby", true));
    }

}
