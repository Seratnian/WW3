using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool open;

    public void Interact()
    {
        if (open)
            transform.eulerAngles = (new Vector3(0, 90, 0));
        else
            transform.eulerAngles = (new Vector3(0, 0, 0));

        open = !open;
    }
}