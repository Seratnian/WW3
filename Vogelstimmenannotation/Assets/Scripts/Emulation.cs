using UnityEngine;
using System.Collections;

public class Emulation : MonoBehaviour {
    public bool isActive = false;
    public GameObject addForceTo;
    public int force = 1;

    private bool forceActivated = false;
    private int activeObjectIndex = -1;

	void Start () {

	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            forceActivated = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            forceActivated = false;
        }

        if (Input.GetKeyDown(KeyCode.Delete)) activeObjectIndex = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)) activeObjectIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) activeObjectIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) activeObjectIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) activeObjectIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) activeObjectIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6)) activeObjectIndex = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7)) activeObjectIndex = 6;
        if (Input.GetKeyDown(KeyCode.Alpha8)) activeObjectIndex = 7;
        if (Input.GetKeyDown(KeyCode.Alpha9)) activeObjectIndex = 8;
        if (Input.GetKeyDown(KeyCode.Alpha0)) activeObjectIndex = 9;
        if (Input.GetKeyDown(KeyCode.Q)) activeObjectIndex = 10;
        if (Input.GetKeyDown(KeyCode.W)) activeObjectIndex = 11;
        if (Input.GetKeyDown(KeyCode.E)) activeObjectIndex = 12;

        if (forceActivated)
        {
            Vector3 direction = new Vector3(1, 0, 1);
            if (activeObjectIndex >= 0)
                addForceTo.GetComponentsInChildren<Rigidbody>()[activeObjectIndex].AddForce(direction * force);
            else
                GameObject.Find("new Profile").GetComponent<Rigidbody>().AddForce(direction * force);
        }
	}
}
