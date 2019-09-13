using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSelector : MonoBehaviour
{
    public Image image;
    public Button button;
    public TextMeshProUGUI plus;
    public Sprite circle;
    public AudioClip click;
    public AudioClip error;
    public AudioSource audioSource;


    private Character character = null;
    private int index;

    private void Awake()
    {
        button.onClick.AddListener(ClickSound);
    }

    public void SetButtonEnable(bool enabled)
    {
        character = null;
        image.sprite = circle;
        button.interactable = enabled;
        plus.enabled = enabled;
    }

    public bool IsButtonEnable()
    { return button.interactable; }

    public IEnumerator Flash(bool sound)
    {
        if (sound)
        {
            audioSource.clip = error;
            audioSource.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            plus.color = new Color32(244, 67, 54, 255);
            yield return new WaitForSeconds(0.08f);
            plus.color = new Color(1f, 1f, 1f);
            yield return new WaitForSeconds(0.08f);
        }
    }

    public void ClickSound()
    {
        audioSource.clip = click;
        audioSource.Play();
    }

    public Character GetCharacter()
    { return character; }

    public void SetCharacter(Character character)
    {
        this.character = character;
        image.sprite = character.sprite;
        plus.enabled = false;
    }

    public int GetIndex()
    { return index; }

    public void SetIndex(int index)
    { this.index = index; }
}
