using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image background;
    public Button rulesButton;
    public Button homeButton;
    public Button backButton;

    private BoardManager boardManager;

    private void Awake()
    {
        rulesButton.onClick.AddListener(Rules);
        homeButton.onClick.AddListener(Home);
        backButton.onClick.AddListener(Back);
    }

    private void Home()
    {
        boardManager.menuSoundEffect.Click();
        GameManager.instance.SetCharacterList(null);
        StartCoroutine(boardManager.MusicFade(-1f, 1f, 1));
        StartCoroutine(GameManager.instance.LoadScene("Lobby"));
    }

    private void Back()
    {
        boardManager.menuSoundEffect.Click();
        boardManager.pause = false;
        Destroy(gameObject);
    }

    private void Rules()
    {
        boardManager.menuSoundEffect.Click();
        Destroy(gameObject);
        boardManager.rulesMenu = Instantiate(boardManager.rulesMenuPrefab, boardManager.boardUI.transform);
        boardManager.rulesMenu.GetComponent<RulesMenu>().boardManager = boardManager;
    }

    public void SetBoardManager(BoardManager bm)
    { boardManager = bm; }
}
