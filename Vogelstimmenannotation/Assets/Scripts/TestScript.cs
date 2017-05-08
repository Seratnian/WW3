using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{

    public string SceneToLoad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        Debug.Log("Mouse is hovering.");
    }

    void OnMouseDown()
    {
        Debug.Log("Object clicked.");
        SceneManager.LoadScene(SceneToLoad);
    }
}
