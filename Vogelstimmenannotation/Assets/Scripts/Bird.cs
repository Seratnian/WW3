using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class Bird : MonoBehaviour
{
    [SerializeField]private AudioSource _audioSource;
    private static BirdDatabaseHandler _birdDatabaseHandler;    

	private void Start ()
	{
	    if (_birdDatabaseHandler == null) _birdDatabaseHandler = BadProgrammingScript.GetBirdDatabaseInstance();

	    LoadBirdSong();
	}

    private void Update()
    {
        if(_audioSource.clip==null)
            LoadBirdSong();
    }

    private void LoadBirdSong()
    {
        _audioSource.clip = _birdDatabaseHandler.GetUnknownBirdSong().Clip;
        _audioSource.Play();
    }

    private void Hit()
    {
        Debug.Log("Bird got hit.");
    }
}