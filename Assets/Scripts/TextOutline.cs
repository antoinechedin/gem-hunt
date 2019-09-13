using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextOutline : MonoBehaviour
{
    public TextMeshProUGUI outline;
    public TextMeshProUGUI textMesh;
    public AudioSource audioSource;

    private string text;

    public string GetText()
    { return text; }

    public void SetText(string text)
    {
        this.text = text;
        outline.text = text;
        textMesh.text = text;
    }

    public void SetColor(Color color)
    {
        textMesh.color = color;
    }

    public IEnumerator Flash()
    {
        audioSource.Play();
        for (int i = 0; i < 3; i++)
        {
            SetColor(new Color(1f, 1f, 1f));
            yield return new WaitForSeconds(0.08f);
            SetColor(new Color32(244, 67 ,54, 255));
            yield return new WaitForSeconds(0.08f);
        }
    }
}
