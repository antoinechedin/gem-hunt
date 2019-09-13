using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationButton : MonoBehaviour
{
    public SpriteRenderer sr;
    public AudioSource audioSource;

    private Action action;
    private bool pressed = false;

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            sr.color = new Color(1f, 1f, 1f, 0.15f);
            pressed = true;
        }
    }

    private void OnMouseUp()
    {
        if (!IsPointerOverUIObject())
        {
            sr.color = new Color(1f, 1f, 1f, 0f);
            if (pressed){
                if (action != null) action();
                audioSource.Play();
            }
        }
    }

    private void OnMouseEnter()
    {
        pressed = true;
    }
    
    private void OnMouseExit()
    {
        pressed = false;
    }

    public void SetAction(Action action)
    { this.action = action; }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
