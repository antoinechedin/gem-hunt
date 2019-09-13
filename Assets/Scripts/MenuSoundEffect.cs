using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip plus;
    public AudioClip minus;
    public AudioClip click;

    public void Plus()
    {
        audioSource.clip = plus;
        audioSource.Play();
    }
    public void Minus()
    {
        audioSource.clip = minus;
        audioSource.Play();
    }
    public void Click()
    {
        audioSource.clip = click;
        audioSource.Play();
    }

}
