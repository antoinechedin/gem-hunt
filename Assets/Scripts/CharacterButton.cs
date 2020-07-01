using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;
    public Button button;
    public Character character;
   
    public IEnumerator MoveTo(Vector2 target, float duration, int type)
    {
        float t = 0;
        Vector2 start = rectTransform.anchoredPosition;
        float distancePosition = Vector2.Distance(start, target);
        Vector2 unit = Vector3.Normalize(target - start);

        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float traveled = 0;
            if (type == 0) //In
                traveled = Easing.EaseInCubic(t, 0, distancePosition, duration);
            else if (type == 1) //Out
                traveled = Easing.EaseOutCubic(t, 0, distancePosition, duration);
            else if (type == 2) //InOut
                traveled = Easing.EaseInOutCubic(t, 0, distancePosition, duration);
            rectTransform.anchoredPosition = start + unit * traveled;

            yield return null;
        }
    }

    public IEnumerator Fade(float direction, float duration, int type)
    {
        float t = 0;
        float start = (direction + 1) / 2;

        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float alpha = 0;
            if (type == 0) //In
                alpha = Easing.EaseInQuad(t, 0, 1, duration) * direction;
            else if (type == 1) //Out
                alpha = Easing.EaseOutQuad(t, 0, 1, duration) * direction;
            else if (type == 2) //InOut
                alpha = Easing.EaseInOutQuad(t, 0, 1, duration) * direction;
            else if (type == 3)
                alpha = Easing.EaseLinear(t, 0, 1, duration) * direction;

            image.color = new Color(1, 1, 1, alpha);

            yield return null;
        }
    }
}
