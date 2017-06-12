using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdIdentificationScreen : MonoBehaviour
{
    public Text IdentifiedBird;
    public Text ExcludedBird;

    private GameObject _loadedBird;
    private BirdSong _loadedBirdSong;
    private static BirdDatabaseHandler _birdDatabaseHandler;

    private void Start()
    {
        if (_birdDatabaseHandler == null) _birdDatabaseHandler = BadProgrammingScript.GetBirdDatabaseInstance();
    }

    public void StartBirdIdentification(GameObject bird)
    {
        _loadedBird = bird;        
        _loadedBirdSong = bird.GetComponent<Bird>().BirdSong;
        bird.GetComponent<AudioSource>().spatialBlend = 0;

        gameObject.SetActive(true);
    }

    public void IdentifyBird()
    {
        if (IdentifiedBird.text == string.Empty) return;

        _birdDatabaseHandler.IdentifyBird(_loadedBirdSong, IdentifiedBird.text);

        gameObject.SetActive(false);      
        Destroy(_loadedBird);
    }

    public void ExcludeBird()
    {
        if (ExcludedBird.text == string.Empty) return;

        _birdDatabaseHandler.ExcludeBird(_loadedBirdSong, ExcludedBird.text);

        _loadedBird.GetComponent<AudioSource>().spatialBlend = 0;
        gameObject.SetActive(false);
    }

    public void CancelIdentification()
    {
        _loadedBird.GetComponent<AudioSource>().spatialBlend = 1;
        gameObject.SetActive(false);
    }
}