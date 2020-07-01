using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform livingRoom;
    public Transform warpPad;
    public Transform beach;

    public NavigationButton livingToWarp;
    public NavigationButton livingToBeach;
    public NavigationButton warpToLiving;
    public NavigationButton beachToLiving;

    [HideInInspector] public int roomIndex;
    private Transform[] roomTransforms;
    private bool isMoving;

    private void Awake()
    {
        roomTransforms = new Transform[3] { beach, livingRoom, warpPad };
        roomIndex = 1;
        livingToWarp.SetAction(delegate { StartCoroutine(Move(1)); });
        livingToBeach.SetAction(delegate { StartCoroutine(Move(-1)); });
        warpToLiving.SetAction(delegate { StartCoroutine(Move(-1)); });
        beachToLiving.SetAction(delegate { StartCoroutine(Move(1)); });
        isMoving = false;
    }

    public IEnumerator Move(int direction)
    {
        float duration = 0.8f;

        if (!isMoving)
        {
            isMoving = true;
            roomIndex += direction;
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                if (t > duration)
                    t = duration;
                float distance = Easing.EaseOutCubic(t, 0, roomTransforms[roomIndex].position.x - roomTransforms[roomIndex - direction].position.x, duration);
                transform.position = new Vector3(roomTransforms[roomIndex - direction].position.x + distance, transform.position.y, transform.position.z);
                yield return null;
            }
            isMoving = false;
            yield return null;
        }
    }
}
