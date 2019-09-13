using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SUButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public HorizontalLayoutGroup hlg;
    public int offset;

    private RectOffset padding;
    private RectOffset paddingPressed;

    private void Awake()
    {
        padding = hlg.padding;
        paddingPressed = new RectOffset(padding.left, padding.right, padding.top + offset, padding.bottom - offset);
        GetComponent<Button>().onClick.AddListener(GetComponent<AudioSource>().Play);
    }

    public void SetShadowColor(Color color)
    { GetComponent<Image>().color = color; }

    public void SetTextColor(Color color)
    { text.color = color; }

    public void NormalState()
    {
        image.color = new Color(1, 1, 1);
        hlg.padding = padding;
    }

    public void HighlightedState()
    {
        image.color = new Color(1, 1, 1);
        hlg.padding = padding;
    }

    public void PressedState()
    {
        image.color = new Color(1, 1, 1);
        hlg.padding = paddingPressed;
    }

    public void SelectedState()
    {
        image.color = new Color(1, 1, 1);
        hlg.padding = padding;
    }

    public void DisabledState()
    {
        image.color = new Color(0.5f, 0.5f, 0.5f);
        hlg.padding = padding;
    }
}
