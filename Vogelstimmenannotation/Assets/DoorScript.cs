using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float openingDuration;
    public GameObject door;

    private bool open;
    private bool opening;

    public void Interact()
    {
        if (!opening)
        {
            if (open)
            {
                StartCoroutine(OpenDoor());
            }
            else
            {
                StartCoroutine(OpenDoor());
            }
        }            
    }

    private IEnumerator OpenDoor()
    {
        opening = true;
        Vector3 startRotation = door.transform.localEulerAngles;
        Vector3 endRotation = startRotation;
        if (!open)
            endRotation.y -= 90;
        else
            endRotation.y += 90;

        for (float i = 0; i < openingDuration; i += Time.fixedDeltaTime)
        {
            door.transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, i / openingDuration);
            yield return new WaitForFixedUpdate();
        }

        open = !open;
        opening = false;
    }
}