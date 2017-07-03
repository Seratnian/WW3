using UnityEngine;
using System.Collections;

public class ButtonPositionReset : MonoBehaviour
{
    private Vector3 originalPosition;
    new private Rigidbody rigidbody;
    private int force;
    private int forceToStop;
    private bool isInContact = false;

    void Start()
    {
        force = 100;
        forceToStop = force * force;
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isInContact)
            Stop(true);
        else
        {
            Vector3 direction = originalPosition - transform.position;
            if (direction.z > 0)
            {
                rigidbody.AddForce(direction * forceToStop);  
            }
        }
            
    }

    void Stop(bool addForce = false)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        if (addForce)
        {
            Vector3 direction = originalPosition - transform.position;
            Debug.Log(direction);
            rigidbody.AddForce(direction * force);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<ButtonPush>() != null)
        {
            isInContact = true;
            ButtonPush buttonPusher = collider.GetComponent<ButtonPush>();
            SteamVR_TrackedObject trackedObj = collider.GetComponent<SteamVR_TrackedObject>();
            SteamVR_Controller.Device Controller = SteamVR_Controller.Input((int)trackedObj.index);
            if (buttonPusher.IsTriggerPressed)
            {
                Vector3 direction = Controller.velocity;
                rigidbody.AddForce(direction * force); 
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<ButtonPush>() != null)
        {
            isInContact = false;
        }
    }
}