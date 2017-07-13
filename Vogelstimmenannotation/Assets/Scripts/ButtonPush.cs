using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPush : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public bool IsTriggerPressed = false;
    public bool IsGrabPressed = false;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            IsTriggerPressed = true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            IsTriggerPressed = false;
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            IsGrabPressed = true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            IsGrabPressed = false;
        }
    }
}
