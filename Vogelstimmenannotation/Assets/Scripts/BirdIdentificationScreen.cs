using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BirdIdentificationScreen : MonoBehaviour
{
    public InputField IdentifyBirdText;
    public InputField ExcludeBirdText;
    public Text ExcludedBirds;

    [SerializeField]private GameObject IdentificationScreen;
    private GameObject _loadedBird;
    private BirdSong _loadedBirdSong;
    private static BirdDatabaseHandler _birdDatabaseHandler;
    private const string BirdListPath = "C:/WW3/birdlist";

    private void Start()
    {
        if (_birdDatabaseHandler == null) _birdDatabaseHandler = BadProgrammingScript.GetBirdDatabaseInstance();
        EventCatalogue.BirdHit += OnBirdHitEvent;
        IdentificationScreen.SetActive(false);
    }

    private void OnBirdHitEvent(System.Object obj, EventArgs eventArgs)
    {
        StartBirdIdentification(obj as GameObject);
    }

    private void OpenIdentificationScreen()
    {
        IdentificationScreen.SetActive(true);
        DisplayExcludedBirds();
        EventCatalogue.OnIdentificationOpened(this, EventArgs.Empty);
    }

    private void CloseIdentificationScreen()
    {
        IdentificationScreen.SetActive(false);
        EventCatalogue.OnIdentificationClosed(this, EventArgs.Empty);
    }

    private void DisplayExcludedBirds()
    {
        Debug.Log("Displaying excluded birds..");
        string birds = String.Empty;
        string[] exclusionList = _loadedBirdSong.IsNotBird;
        foreach (string s in exclusionList)
        {
            birds += s +", ";
        }
        ExcludedBirds.text += birds;
    }

    public void StartBirdIdentification(GameObject bird)
    {
        _loadedBird = bird;        
        _loadedBirdSong = bird.GetComponent<Bird>().BirdSong;
        bird.GetComponent<AudioSource>().spatialBlend = 0;

        OpenIdentificationScreen();
    }

    public void IdentifyBird()
    {
        if (IdentifyBirdText.text == string.Empty) return;

        _birdDatabaseHandler.IdentifyBird(_loadedBirdSong, IdentifyBirdText.text);
        AddBirdToList();

        CloseIdentificationScreen();
        Destroy(_loadedBird);
    }

    private void AddBirdToList()
    {
        if (!File.Exists(BirdListPath))
        {
            List<string> birdList = new List<string>();
            birdList.Add(IdentifyBirdText.text);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(birdList);
            File.WriteAllText(BirdListPath, json);
        }
        else
        {
            string json = File.ReadAllText(BirdListPath);
            List<string> birdList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(json);
            birdList.Add(IdentifyBirdText.text);
            json = Newtonsoft.Json.JsonConvert.SerializeObject(birdList);
            File.WriteAllText(BirdListPath, json);
        }
    }

    public void ExcludeBird()
    {
        if (ExcludeBirdText.text == string.Empty) return;

        _birdDatabaseHandler.ExcludeBird(_loadedBirdSong, ExcludeBirdText.text);

        _loadedBird.GetComponent<AudioSource>().spatialBlend = 1;

        CloseIdentificationScreen();
    }

    public void CancelIdentification()
    {
        _loadedBird.GetComponent<AudioSource>().spatialBlend = 1;

        CloseIdentificationScreen();
    }
}