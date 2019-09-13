using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SUTitle : MonoBehaviour
{
    public TextMeshProUGUI shadow;
    public TextMeshProUGUI outline;
    public TextMeshProUGUI textMesh;

    public void SetText(string text)
    {
        shadow.text = text;
        outline.text = text;
        textMesh.text = text;
    }

    public void SetTextColor(Color color)
    { textMesh.color = color; }

    public void SetShadowColor(Color color)
    { shadow.color = color; }
}
