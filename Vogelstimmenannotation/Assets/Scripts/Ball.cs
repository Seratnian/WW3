using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ThrowForce;
    public float TimeOut;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 direction)
    {
        _rigidbody.AddForce(direction * ThrowForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision col)
    {
        col.gameObject.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
        DestroyBallAfterTimeOut();
    }

    private void DestroyBallAfterTimeOut()
    {
        Destroy(gameObject, TimeOut);
    }
}