using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TEST_Events : MonoBehaviour
{

    public string[] eventNames = new string[1];

	// Use this for initialization
	void Start () {
        foreach (string eventName in eventNames)
        {
            EventManager.StartListening(eventName, LogToConsole);
        }
	}

    void LogToConsole(string eventName)
    {
        Debug.Log(eventName + " was called!");
    }
}