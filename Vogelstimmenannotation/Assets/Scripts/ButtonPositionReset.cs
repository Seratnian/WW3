using UnityEngine;
using System.Collections;

public class ButtonPositionReset : MonoBehaviour
{
    public Vector3 originalPosition;
    new private Rigidbody rigidbody;
    public int resetForce = 100;
    public float distance = 0.02f;
    public bool moveX = false;
    public bool moveY = false;
    public bool moveZ = true;
    private Vector3 origControllerPosition = Vector3.zero;

    void Start()
    {
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        Vector3 direction = originalPosition - transform.position;
        rigidbody.AddForce(direction * resetForce);
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<ButtonPush>() != null)
        {
            ButtonPush buttonPusher = collider.GetComponent<ButtonPush>();
            SteamVR_TrackedObject trackedObj = collider.GetComponent<SteamVR_TrackedObject>();
            SteamVR_Controller.Device Controller = SteamVR_Controller.Input((int)trackedObj.index);
            if (buttonPusher.IsTriggerPressed)
            {
                if (origControllerPosition.Equals(Vector3.zero))
                {
                    origControllerPosition = buttonPusher.transform.position;
                }
                Vector3 movement = buttonPusher.transform.position - origControllerPosition;
                movement.x = moveX ? Mathf.Min(Mathf.Max(0, movement.x), distance) : 0;
                movement.y = moveY ? Mathf.Min(Mathf.Max(0, movement.y), distance) : 0;
                movement.z = moveZ ? Mathf.Min(Mathf.Max(0, movement.z), distance) : 0;
                rigidbody.transform.position = originalPosition + movement;
            }
        }
    }
}