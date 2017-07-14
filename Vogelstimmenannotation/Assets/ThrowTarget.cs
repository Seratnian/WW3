using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTarget : MonoBehaviour {
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
	}

    private void OnCollisionEnter(Collision collision)
    {
        CanBeThrown ball = collision.gameObject.GetComponent<CanBeThrown>();
        Debug.Log(ball);
        Debug.Log(!ball);
        if (ball)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
