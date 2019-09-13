using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterResult : MonoBehaviour
{
    public Image characterImage;
    public SUTitle place;
    public TextMeshProUGUI score;
    public SUTitle bubbleCounter;
    public SUTitle footprintsCounter;
    public SUTitle clawsCounter;

    public void SetCharacter(Character character, int placeNumber = 0)
    {
        if (character == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            characterImage.sprite = character.sprite;
            score.text = character.gameStat.score.ToString();
            place.SetText(string.Format("#{0}", placeNumber));
            place.SetTextColor(character.color);
            bubbleCounter.SetText(character.gameStat.bubbleCounter.ToString());
            footprintsCounter.SetText(character.gameStat.footprintsCounter.ToString());
            clawsCounter.SetText(character.gameStat.clawsCounter.ToString());
        }
    }
}
