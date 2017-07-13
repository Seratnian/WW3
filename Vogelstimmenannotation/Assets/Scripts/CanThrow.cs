using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanThrow : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;

    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update () {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<CanBeThrown>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        objectInHand.GetComponent<CanBeThrown>().Grab();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<CanBeThrown>().Release(Controller.velocity, Controller.angularVelocity);
        }
        objectInHand = null;
    }
}
