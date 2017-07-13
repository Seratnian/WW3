using System;
using UnityEngine;

public class CanBeThrown : MonoBehaviour
{
    new Rigidbody rigidbody;
    ItemHolder holder;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        holder = transform.parent.GetComponent<ItemHolder>();
    }

    internal void Grab()
    {
        holder.RemoveItem(gameObject);

        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        transform.parent = null;
    }

    internal void Release(Vector3 velocity, Vector3 angularVelocity)
    {
        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
    }
}
