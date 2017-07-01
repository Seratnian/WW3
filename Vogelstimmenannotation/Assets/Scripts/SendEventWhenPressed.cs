using UnityEngine;
using System.Collections;

public class SendEventWhenPressed : MonoBehaviour
{
    public double distance = 0.05;
    public string eventName;
    public string eventData = "this.name";

    private bool eventCalled = false;

    private Vector3 originalPosition;

	void Start () {
        originalPosition = transform.position;
        if (eventData.Contains("this.name")) eventData = this.name;
	}
	
	void Update ()
    {
        if (!eventCalled)
        {
            if (Vector3.Distance(originalPosition, transform.position) > distance)
            {
                EventManager.TriggerEvent(eventName, eventData);
                eventCalled = true;
            }
        }
        else
        {
            if (Vector3.Distance(originalPosition, transform.position) < distance)
            {
                eventCalled = false;
            }
        }
	}
}
