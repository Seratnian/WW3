using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    private string _birdListPath;

	private void Start ()
	{
	    _birdListPath = "C:/WW3/birdlist";

        if (!File.Exists(_birdListPath))
	    {
	        List<string> birdList = new List<string>() {"Cuckoo", "Owl", "Nightingale", "RandomBird"};
	        string json = Newtonsoft.Json.JsonConvert.SerializeObject(birdList);
	        File.WriteAllText(_birdListPath, json);
	    }

	    CreateButtonsList();
	}

    public void CreateButtonsList()
    {
        List<string> birdList = LoadAndGetBirdList();

        foreach (string bird in birdList)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.name = bird;
            newButton.transform.SetParent(transform);
            newButton.transform.localScale = Vector3.one;
            newButton.transform.GetChild(0).GetComponent<Text>().text = bird;
            newButton.SetActive(true);
        }
    }

    private List<string> LoadAndGetBirdList()
    {
        string json = File.ReadAllText(_birdListPath);
        Debug.Log(json);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json);
    }
}