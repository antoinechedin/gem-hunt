using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] greenSprites;
    public Sprite[] orangeSprites;
    public Sprite[] redSprites;

    [HideInInspector] public Dictionary<string, Sprite[]> spriteDicrionary;
    [HideInInspector] public int[] faceArray;
    [HideInInspector] public string color;
    [HideInInspector] public Animator animator;

    private int faceIndex = 0;
    private float size = 1.22f;

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        transform.localScale = new Vector2(0, 0);

        rb2D = this.GetComponent<Rigidbody2D>();
        bc2D = this.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        spriteDicrionary = new Dictionary<string, Sprite[]>();
        spriteDicrionary.Add("green", greenSprites);
        spriteDicrionary.Add("orange", orangeSprites);
        spriteDicrionary.Add("red", redSprites);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetColor(string c)
    {
        color = c;
        switch (c)
        {
            case "green":
                faceArray = new int[6] { 0, 0, 0, 1, 1, 2 };
                break;
            case "orange":
                faceArray = new int[6] { 0, 0, 1, 1, 2, 2 };
                break;
            case "red":
                faceArray = new int[6] { 0, 1, 1, 2, 2, 2 };
                break;
            default:
                throw new KeyNotFoundException(string.Format("Can't set the dice color to \"{0}\".", c));
        }
    }

    public IEnumerator Roll(bool grow, string type)
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        bc2D.enabled = true;

        if (type == "player")
        {
            faceIndex = faceArray[Random.Range(0, faceArray.Length)];
        }
        else
        {
            if (Random.Range(0, 7) == 0)
                faceIndex = 2;
            else
                faceIndex = faceArray[Random.Range(0, faceArray.Length)];
        }
        spriteRenderer.sprite = spriteDicrionary[color][faceIndex];
        spriteRenderer.color = new Color(1, 1, 1);

        float torque;
        if (grow)
            torque = 1.5f;
        else
            torque = 70f;
        torque *= Random.Range(0, 2) * 2 - 1; //Random direction
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        rb2D.AddTorque(torque);

        if (grow)
            StartCoroutine(Grow());
        else
            StartCoroutine(Highlight());
        yield return null;
    }

    private IEnumerator Grow()
    {
        float duration = 0.7f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, size, duration);
            transform.localScale = new Vector2(x, x);
            yield return null;
        }
    }

    public IEnumerator Shrink()
    {
        float duration = 0.4f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0f, size, duration);
            transform.localScale = new Vector2(size - x, size - x);
            yield return null;
        }
    }

    public IEnumerator Highlight()
    {
        float duration = 0.7f;
        float grow = 0.3f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0, grow, duration);
            transform.localScale = new Vector2(size + grow - x, size + grow - x);
            yield return null;
        }
    }

    public IEnumerator Lowlight()
    {
        float duration = 0.4f;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            t = t > duration ? duration : t; //Bound t to duration

            float x = Easing.EaseOutCubic(t, 0, 0.3f, duration);
            spriteRenderer.color = new Color(1f - x, 1f - x, 1f - x);
            yield return null;
        }
    }

    public IEnumerator MoveTo(Vector2 target, float duration)
    {
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        bc2D.enabled = false;
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0;

        float t = 0;

        Vector2 startPosition = transform.position;
        float distancePosition = Vector2.Distance(startPosition, target);
        Vector2 unitPosition = Vector3.Normalize(target - startPosition);

        float startAngle = transform.rotation.eulerAngles.z;
        float targetAngle = startAngle > 180 ? 360 : 0;
        float distanceAngle = targetAngle - startAngle;

        while (t < duration)
        {
            t += Time.deltaTime;
            if (t > duration)
                t = duration;
            float traveled = Easing.EaseOutCubic(t, 0, distancePosition, duration);
            float rotated = Easing.EaseOutCubic(t, startAngle, distanceAngle, duration);

            rb2D.MovePosition(startPosition + unitPosition * traveled);
            rb2D.MoveRotation(rotated);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator MoveToRelative(Vector2 movement, float duration)
    {
        Vector2 target = (Vector2)transform.position + movement;
        yield return MoveTo(target, duration);
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetColor()
    {
        return color;
    }

    public int GetFaceIndex()
    {
        return faceIndex;
    }
}
