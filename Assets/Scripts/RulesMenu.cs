using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesMenu : MonoBehaviour
{
    public Button nextButton;
    public Button prevButton;
    public GameObject[] panels;

    private int index = 0;
    [HideInInspector] public BoardManager boardManager;

    private void Awake()
    {
        nextButton.onClick.AddListener(Next);
        prevButton.onClick.AddListener(Prev);
        prevButton.gameObject.SetActive(false);
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == 0);
    }

    public void Next()
    {
        boardManager.menuSoundEffect.Plus();
        if (index == panels.Length - 1)
        {
             boardManager.menuSoundEffect.Click();
            boardManager.pause = false;
            Destroy(gameObject);
        }
        else
        {
            boardManager.menuSoundEffect.Plus();
            panels[index].SetActive(false);
            index++;
            panels[index].SetActive(true);
            if (index == panels.Length - 1)
                nextButton.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "x";
            prevButton.gameObject.SetActive(true);
        }
    }
    public void Prev()
    {
         boardManager.menuSoundEffect.Minus();
        if (index >= 1)
        {
            panels[index].SetActive(false);
            index--;
            panels[index].SetActive(true);
            if (index == 0)
                prevButton.gameObject.SetActive(false);
            nextButton.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = ">";
        }
    }

}
