using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    ArrayList buttons = new ArrayList();
    int ButtonsToBePressed;
    UnityAction nextAction;

	void Start ()
    {
        EventManager.StartListening("PlaySound", SoundButtonPressed);
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            //store buttons for later use
            buttons.Add(rigidbody.gameObject);
        }
        StartThirdLevel();
	}

    private void StartFirstLevel()
    {
        // disable the last two buttons
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            //store buttons for later use
            buttons.Add(rigidbody.gameObject);
            if (rigidbody.gameObject.name.EndsWith("2") || rigidbody.gameObject.name.EndsWith("3"))
                rigidbody.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        ButtonsToBePressed = buttons.Count / 3;
        nextAction = StartSecondLevel;
    }

    private void SoundButtonPressed(object data)
    {
        SendEventWhenPressed button = data as SendEventWhenPressed;
        if (!button.WasCalledAtLeastOnce)
        {
            ButtonsToBePressed--;
            if (ButtonsToBePressed <= 0)
            {
                nextAction.Invoke();
            }
        }
    }

    private void StartSecondLevel()
    {
        // disable the first button, enable the rest
        foreach (GameObject button in buttons)
        {
            if (button.name.EndsWith("1"))
                button.GetComponent<MeshRenderer>().enabled = false;
            else
                button.GetComponent<MeshRenderer>().enabled = true;
            button.GetComponent<SendEventWhenPressed>().WasCalledAtLeastOnce = false;
        }
        ButtonsToBePressed = buttons.Count * 2 / 3;
        nextAction = StartThirdLevel;
    }

    private void StartThirdLevel()
    {
        // enable all buttons
        foreach (GameObject button in buttons)
        {
            button.GetComponent<MeshRenderer>().enabled = true;
            button.GetComponent<SendEventWhenPressed>().WasCalledAtLeastOnce = false;
        }
        ButtonsToBePressed = buttons.Count;
        nextAction = StartFourthLevel;

    }

    private void StartFourthLevel()
    {
        Debug.Log("Vögel abwerfen und so...");
    }

}
