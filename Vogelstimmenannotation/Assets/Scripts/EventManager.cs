using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ObjectEvent : UnityEvent<object>
{

};

public class EventManager : MonoBehaviour
{

    private Dictionary<string, ObjectEvent> eventDictionary;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                    Debug.LogError("No EventManager found.");
                else
                    eventManager.Init();
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<string, ObjectEvent>();
    }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        ObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.AddListener(listener);
        else
        {
            thisEvent = new ObjectEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (eventManager == null)
            return;
        ObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public static void TriggerEvent(string eventName, object data = null)
    {
        ObjectEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            Debug.Log("Event: " + eventName);
            thisEvent.Invoke(data);
        }
        else
        {
            Debug.Log("No event found.");
        }
    }
}
