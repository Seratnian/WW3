using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    private ArrayList buttons = new ArrayList();
    private int ButtonsToBePressed;
    private Vector3 shufflePosition;
    private UnityAction[] nextAction;
    private int actionIndex = 0;

    public UnityAction NextAction
    {
        get
        {
            return nextAction[actionIndex++];
        }
    }

    void Start()
    {
        shufflePosition = (GameObject.Find("ShuffleTarget") as GameObject).transform.position;
        nextAction = new UnityAction[]
        {
            StartFirstLevel,
            StartSecondLevel,
            StartThirdLevel,
            StartFourthLevel
        };
        EventManager.StartListening("PlaySound", SoundButtonPressed);
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            //store buttons for later use
            buttons.Add(rigidbody.gameObject);
        }
        NextAction.Invoke();
    }

    private void SoundButtonPressed(object data)
    {
        SendEventWhenPressed button = data as SendEventWhenPressed;
        if (!button.WasCalledAtLeastOnce)
        {
            ButtonsToBePressed--;
            if (ButtonsToBePressed <= 0)
            {
                NextAction.Invoke();
            }
        }
    }

    private void ShuffleButtons(ArrayList buttons)
    {
        foreach (object buttonObject in buttons)
        {
            Rigidbody button = buttonObject as Rigidbody;
            button.GetComponent<ButtonPositionReset>().originalPosition = shufflePosition;
        }
    }

    private void StartFirstLevel()
    {
        ArrayList activeButtons = new ArrayList();
        // disable the last two buttons
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            if (rigidbody.gameObject.name.EndsWith("2") || rigidbody.gameObject.name.EndsWith("3"))
                rigidbody.gameObject.GetComponent<MeshRenderer>().enabled = false;
            else
                activeButtons.Add(rigidbody);
        }
        ButtonsToBePressed = buttons.Count / 3;
        ShuffleButtons(activeButtons);
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

    }

    private void StartFourthLevel()
    {
        EventManager.StopListening("PlaySound", SoundButtonPressed);
        Debug.Log("Vögel abwerfen und so...");
    }

}
