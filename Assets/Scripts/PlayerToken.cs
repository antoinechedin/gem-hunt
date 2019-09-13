using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utils;

public class PlayerToken : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;
    public RectTransform scoreRectTransform;
    public TextMeshProUGUI scoreText;
    public RectTransform turnScoreRectTransform;
    public TextMeshProUGUI turnScoreText;

    private Character character;
    private int life;
    private int score = 0;
    private int turnScore = 0;

    private void Awake()
    {
        scoreText.color = new Color(1f, 1f, 1f, 0f);
        turnScoreText.color = new Color(1f, 1f, 1f, 0f);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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

    public IEnumerator ShowScore(float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float alpha = Easing.EaseInOutCubic(t, 0, 1, duration);
            scoreText.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
    }

    public IEnumerator pullOutScore(float duration)
    {
        turnScore = 0;
        turnScoreText.text = "+0";
        turnScoreText.enabled = true;
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float x = Easing.EaseOutCubic(t, 0, 70, duration);
            float alpha = Easing.EaseOutCubic(t, 0, 1, duration);

            turnScoreRectTransform.anchoredPosition = new Vector2(x, 0);
            turnScoreText.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    public IEnumerator pullInScore(float duration, BoardSoundsEffect se)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float x = Easing.EaseInCubic(t, 0, 70, duration);
            float alpha = Easing.EaseInCubic(t, 0, 1, duration);

            turnScoreRectTransform.anchoredPosition = new Vector2(70 - x, 0);
            turnScoreText.color = new Color(1, 1, 1, 1 - alpha);
            yield return null;
        }
        score += turnScore;
        scoreText.text = score.ToString();
        turnScoreText.enabled = false;
        se.audioSource.PlayOneShot(se.gain);
        scoreRectTransform.localScale = new Vector2(1.6f, 1.6f);
        t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float x = Easing.EaseOutCubic(t, 0, 0.6f, duration);
            scoreRectTransform.localScale = new Vector2(1.6f - x, 1.6f - x);

            yield return null;
        }

    }

    public IEnumerator loseScore(float duration)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float alpha = Easing.EaseLinear(t, 0, 1, duration);
            turnScoreText.color = new Color(1, 1, 1, 1 - alpha);
            yield return null;
        }
        turnScoreRectTransform.anchoredPosition = new Vector2(0, 0);
        turnScoreText.enabled = false;
    }

    public Vector2 GetTurnScorePostionInWorld()
    {
        return Camera.main.ScreenToWorldPoint(turnScoreRectTransform.transform.position);
    }

    public IEnumerator GainTurnScore(int score)
    {
        float duration = 0.4f;

        turnScore += score;
        turnScoreText.text = "+" + turnScore;
        turnScoreRectTransform.localScale = new Vector2(1.6f, 1.6f);
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float x = Easing.EaseOutCubic(t, 0, 0.6f, duration);
            turnScoreRectTransform.localScale = new Vector2(1.6f - x, 1.6f - x);

            yield return null;
        }
    }

    public Character GetCharacter()
    { return character; }

    public void SetCharacter(Character character)
    {
        this.character = character;
        image.sprite = character.sprite;
    }

    public int GetLife()
    { return life; }

    public void SetLife(int life)
    { this.life = life; }

    public int GetScore()
    { return score; }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    public int GetTurnScore()
    { return turnScore; }

    public void SetturnScore(int turnScore)
    {
        this.turnScore = turnScore;
        turnScoreText.text = string.Format("+{0}", turnScore.ToString());
    }
}
