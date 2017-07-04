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
    private GameObject board;

    public UnityAction NextAction
    {
        get
        {
            return nextAction[actionIndex++];
        }
    }

    void Start()
    {
        board = GameObject.Find("Board") as GameObject;
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

    IEnumerator ShuffleButtons(ArrayList buttons)
    {
        foreach (object buttonObject in buttons)
        {
            Rigidbody button = buttonObject as Rigidbody;
            button.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            button.GetComponent<SendEventWhenPressed>().enabled = false;
            button.GetComponent<ButtonPositionReset>().originalPosition = getRandomVector(shufflePosition);
        }

        yield return new WaitForSeconds(3);

        foreach (object buttonObject in buttons)
        {
            Rigidbody button = buttonObject as Rigidbody;
            button.GetComponent<ButtonPositionReset>().originalPosition = button.transform.position;
        }

        yield return new WaitForSeconds(1);

        foreach (object buttonObject in buttons)
        {
            Rigidbody button = buttonObject as Rigidbody;
            button.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            button.GetComponent<SendEventWhenPressed>().enabled = true;
        }

    }

    private void StartFirstLevel()
    {
        ArrayList activeButtons = new ArrayList();
        // disable the last two buttons
        foreach (Rigidbody rigidbody in board.GetComponentsInChildren<Rigidbody>())
        {
            if (getParentName(rigidbody.gameObject).EndsWith("2") || getParentName(rigidbody.gameObject).EndsWith("3"))
                disableButton(rigidbody.gameObject);
            else
                activeButtons.Add(rigidbody);
        }
        ButtonsToBePressed = buttons.Count / 3;
        //StartCoroutine(ShuffleButtons(activeButtons));
    }

    private void StartSecondLevel()
    {
        // disable the first button, enable the rest
        foreach (GameObject button in buttons)
        {
            if (getParentName(GetComponent<Rigidbody>().gameObject).EndsWith("1"))
                enableButton(button);
            else
                disableButton(button);
            button.GetComponent<SendEventWhenPressed>().WasCalledAtLeastOnce = false;
        }
        ButtonsToBePressed = buttons.Count * 2 / 3;
    }

    private void StartThirdLevel()
    {
        // enable all buttons
        foreach (GameObject button in buttons)
        {
            enableButton(button);
            button.GetComponent<SendEventWhenPressed>().WasCalledAtLeastOnce = false;
        }
        ButtonsToBePressed = buttons.Count;

    }

    private void StartFourthLevel()
    {
        EventManager.StopListening("PlaySound", SoundButtonPressed);
        Debug.Log("Vögel abwerfen und so...");
    }

    private string getParentName(GameObject gameObject)
    {
        return gameObject.transform.parent.gameObject.name;
    }

    private void disableButton(GameObject button)
    {
        button.GetComponent<MeshRenderer>().enabled = false;
        button.GetComponent<BoxCollider>().enabled = false;
    }

    private void enableButton(GameObject button)
    {
        button.GetComponent<MeshRenderer>().enabled = true;
        button.GetComponent<BoxCollider>().enabled = true;
    }

    private Vector3 getRandomVector(Vector3 baseVector = default(Vector3))
    {
        return new Vector3(
            baseVector.x + Random.value * 0,
            baseVector.y + Random.value * 0,
            baseVector.z
        );
    }
}
