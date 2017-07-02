using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
    DataStorage dataStorage;
    Player player;

	void Start ()
    {
        // TODO remove if/else statement
        if (GameObject.Find("DataStorage") != null)
            dataStorage = GameObject.Find("DataStorage").GetComponent<DataStorage>();
        else
        {
            GameObject ds = new GameObject("DataStorage");
            dataStorage = ds.AddComponent<DataStorage>();
            dataStorage.Profile = new Profile("TestPlayer");
        }
        player = GameObject.Find("Player").GetComponent<Player>();
        player.name = dataStorage.Profile.Name;

        if (!dataStorage.Profile.FinishedTutorial)
        {
            GameObject Tutorial = Instantiate(Resources.Load("Tutorial"), GameObject.Find("Level").transform) as GameObject;
            // TODO remove
            GameObject.Find("Emulator").GetComponent<Emulation>().addForceTo = Tutorial.transform.GetChild(0).gameObject;
            // TODO
        }
	}

}
