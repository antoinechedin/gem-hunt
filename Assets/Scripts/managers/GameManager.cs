using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image foreground;

    [HideInInspector] public bool fading = true;
    private List<Character> characterList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        characterList = new List<Character>();
        foreground.gameObject.SetActive(true);

    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("lang"))
        {
            LocalizationManager.instance.LoadLocalizedText(PlayerPrefs.GetString("lang"));
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            StartCoroutine(FadeOut(1f));
        }
    }

    public IEnumerator LoadScene(string name, bool waitLocalization = false)
    {
        yield return StartCoroutine(FadeIn(1f));
        while (waitLocalization && !LocalizationManager.instance.GetIsReady()) yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator FadeIn(float duration)
    {
        fading = true;
        Color c = foreground.color;
        foreground.raycastTarget = true;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float a = Easing.EaseLinear(t, 0f, 1f, duration);
            foreground.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }
    }

    public IEnumerator FadeOut(float duration)
    {
        yield return new WaitForSeconds(0.05f);
        Color c = foreground.color;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float a = Easing.EaseLinear(t, 0f, 1f, duration);
            foreground.color = new Color(c.r, c.g, c.b, 1f - a);
            yield return null;
        }
        foreground.raycastTarget = false;
        fading = false;
        yield return null;
    }

    public List<Character> GetCharacterList()
    { return characterList; }

    public void SetCharacterList(List<Character> characterList)
    { this.characterList = characterList; }
}
