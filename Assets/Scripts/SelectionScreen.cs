using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SelectionScreen : MonoBehaviour
{
    public Image background;
    public CharacterButton[] characters;
    public int lineLength;

    private CharacterButton[,] characterMatrix;

    private void Awake()
    {
        characterMatrix = new CharacterButton[characters.Length / lineLength + 1, lineLength];
        for (int k = 0; k < characters.Length; k++)
        {
            int i = k / lineLength;
            int j = k % lineLength;
            characterMatrix[i, j] = characters[k];
            characters[k].character.id = k;
            characters[k].character.type = "player";
            characters[k].image.raycastTarget = false;
            characters[k].image.color = new Color(0f, 0f, 0f, 0f);
        }
        Color c = background.color;
        background.color = new Color(c.r, c.g, c.b, 0f);
        background.raycastTarget = false;
    }

    public IEnumerator FadeInBackground()
    {
        float duration = 0.3f;

        Color c = background.color;
        background.color = new Color(c.r, c.g, c.b, 0f);
        background.raycastTarget = true;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float a = Easing.EaseOutQuad(t, 0f, 0.8f, duration);
            background.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }
    }

    public IEnumerator FadeOutBackground()
    {
        float duration = 0.2f;

        Color c = background.color;
        background.color = new Color(c.r, c.g, c.b, 0f);
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float a = Easing.EaseLinear(t, 0f, 0.8f, duration);
            background.color = new Color(c.r, c.g, c.b, 0.8f - a); 
            yield return null;
        }
        background.raycastTarget = false;
        yield return null;
    }

    public IEnumerator ShowCharacterButtons()
    {
        Vector2 offset = new Vector2(30, -30);

        for (int k = 0; k <= characterMatrix.GetLength(0) + characterMatrix.GetLength(1) - 2; k++)
        {
            for (int j = 0; j <= k; j++)
            {
                int i = k - j;
                if (i < characterMatrix.GetLength(0) && j < characterMatrix.GetLength(1))
                {
                    CharacterButton cb = characterMatrix[i, j];
                    if (cb != null)
                    {
                        cb.image.raycastTarget = true;
                        cb.image.color = new Color(1, 1, 1, 0);
                        //cb.rectTransform.anchoredPosition = cb.rectTransform.anchoredPosition + offset;

                        //StartCoroutine(cb.MoveTo(cb.rectTransform.anchoredPosition - offset, 0.3f, 1));
                        StartCoroutine(cb.Fade(1, 0.3f, 1));
                    }
                }
            }

            //yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator HideCharacterButtons()
    {
        foreach (CharacterButton cb in characters)
        {
            StartCoroutine(cb.Fade(-1, 0.2f, 3));
        }
        yield return new WaitForSeconds(0.2f);
        foreach(CharacterButton cb in characters)
        {
            cb.image.raycastTarget = false;
        }
        yield return null;
    }
}
