using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float openingDuration;

    private bool open;
    private bool opening;

    public void Interact()
    {
        if (!opening)
        {
            if (open)
            {
                StartCoroutine(OpenDoor(new Vector3(0, 0, 0)));
            }
            else
            {
                StartCoroutine(OpenDoor(new Vector3(0, 90, 0)));
            }
        }            
    }

    private IEnumerator OpenDoor(Vector3 endRotation)
    {
        opening = true;
        Vector3 startRotation = transform.eulerAngles;

        for (float i = 0; i < openingDuration; i += Time.fixedDeltaTime)
        {
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, i / openingDuration);
            yield return new WaitForFixedUpdate();
        }

        open = !open;
        opening = false;
    }
}