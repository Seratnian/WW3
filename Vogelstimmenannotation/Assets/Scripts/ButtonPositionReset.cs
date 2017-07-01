using UnityEngine;
using System.Collections;

public class ButtonPositionReset : MonoBehaviour
{
    private Vector3 originalPosition;
    new private Rigidbody rigidbody;
    private int force = 100;

	void Start ()
    {
        originalPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        Stop(true);
	}

    void Stop(bool addForce = false)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        if (addForce)
        {
            Vector3 direction = originalPosition - transform.position;
            rigidbody.AddForce(direction * force);
        }
    }
}
