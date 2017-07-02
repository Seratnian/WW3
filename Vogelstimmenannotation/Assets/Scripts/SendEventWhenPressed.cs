using UnityEngine;
using System.Collections;

public class SendEventWhenPressed : MonoBehaviour
{
    public double distance = 0.05;
    public string eventName;
    public object eventData = null;

    private bool eventCalled = false;
    public bool WasCalledAtLeastOnce { get; set; }

    private Vector3 originalPosition;

	void Start () {
        originalPosition = transform.position;
        WasCalledAtLeastOnce = false;
	}
	
	void Update ()
    {
        if (!eventCalled)
        {
            if (Vector3.Distance(originalPosition, transform.position) > distance)
            {
                if (eventData == null) eventData = this;
                EventManager.TriggerEvent(eventName, eventData);
                eventCalled = true;
                WasCalledAtLeastOnce = true;
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
