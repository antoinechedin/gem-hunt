using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeachManager : MonoBehaviour
{
    public Button french;
    public Button english;
    public Button spanish;
    public Button portuguese;
    public Button russian;
    public AudioSource audioSource;

    private Dictionary<string, Button> flags;

    private void Awake()
    {
        flags = new Dictionary<string, Button>();
        flags.Add("french", french);
        flags.Add("english", english);
        flags.Add("spanish", spanish);
        flags.Add("portuguese", portuguese);
        flags.Add("russian", russian);
    }

    private void Start()
    {
        french.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(SetLangTo("french")); });
        english.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(SetLangTo("english")); });
        spanish.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(SetLangTo("spanish")); });
        portuguese.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(SetLangTo("portuguese")); });
        russian.GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(SetLangTo("russian")); });

        flags[PlayerPrefs.GetString("lang")].interactable = false;
    }

    private IEnumerator SetLangTo(string lang)
    {
        audioSource.Play();
        flags[PlayerPrefs.GetString("lang")].interactable = true;
        flags[lang].interactable = false;
        PlayerPrefs.SetString("lang", lang);
        PlayerPrefs.Save();
        LocalizationManager.instance.LoadLocalizedText(lang);
        while(!LocalizationManager.instance.GetIsReady())
            yield return null;
        LocalizationManager.instance.UpdateAllText();
        yield return null;
    }
}
