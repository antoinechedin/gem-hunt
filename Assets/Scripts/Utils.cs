using System;
using System.Collections.Generic;

public class Easing
{
    public static float EaseLinear(float t, float b, float c, float d)
    {
        return c * t / d + b;
    }

    public static float EaseInQuad(float t, float b, float c, float d)
    {
        t /= d;
        return c * t * t + b;
    }

    public static float EaseOutQuad(float t, float b, float c, float d)
    {
        t /= d;
        return -c * t * (t - 2) + b;
    }

    public static float EaseInOutQuad(float t, float b, float c, float d)
    {
        t /= d / 2;
        if (t < 1) return c / 2 * t * t + b;
        t--;
        return -c / 2 * (t * (t - 2) - 1) + b;
    }

    public static float EaseInCubic(float t, float b, float c, float d)
    {
        t /= d;
        return c * t * t * t + b;
    }

    public static float EaseOutCubic(float t, float b, float c, float d)
    {
        t /= d;
        t--;
        return c * (t * t * t + 1) + b;
    }

    public static float EaseInOutCubic(float t, float b, float c, float d)
    {
        t /= d / 2;
        if (t < 1) return c / 2 * t * t * t + b;
        t -= 2;
        return c / 2 * (t * t * t + 2) + b;
    }

    public static float EaseInQuart(float t, float b, float c, float d)
    {
        t /= d;
        return c * t * t * t * t + b;
    }

    public static float EaseOutQuart(float t, float b, float c, float d)
    {
        t /= d;
        t--;
        return -c * (t * t * t * t - 1) + b;
    }

    public static float EaseInOutQuart(float t, float b, float c, float d)
    {
        t /= d / 2;
        if (t < 1) return c / 2 * t * t * t * t + b;
        t -= 2;
        return -c / 2 * (t * t * t * t - 2) + b;
    }

    public static float EaseInOutSine(float t, float b, float c, float d)
    {
        return (float)(-c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b);
    }

    public static float EaseInOutExpo(float t, float b, float c, float d)
    {
        t /= d / 2;
        if (t < 1) return (float)(c / 2 * Math.Pow(2, 10 * (t - 1)) + b);
        t--;
        return (float)(c / 2 * (-Math.Pow(2, -10 * t) + 2) + b);
    }
}

public class Utils
{
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static string ReverseEachWord(string s)
    {
        string[] words = s.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = Reverse(words[i]);
        }
        return String.Join(" ", words);
    }
}


