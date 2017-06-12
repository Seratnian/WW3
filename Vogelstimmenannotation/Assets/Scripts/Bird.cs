using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class Bird : MonoBehaviour
{    
    public BirdSong BirdSong { private set; get; }

    private AudioSource _audioSource { get { return m_audioSource ?? (m_audioSource = GetComponent<AudioSource>()); } }
    private AudioSource m_audioSource;
    private static BirdDatabaseHandler _birdDatabaseHandler;
    private static BirdIdentificationScreen _birdIdentificationScreen;
    private static GameObject _player;

	private void Start ()
	{
	    if (_birdDatabaseHandler == null) _birdDatabaseHandler = BadProgrammingScript.GetBirdDatabaseInstance();
	    if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
	    if (_birdIdentificationScreen == null)
	        _birdIdentificationScreen = GameObject.Find("BirdIdentificationScreen").GetComponent<BirdIdentificationScreen>();

	    LoadBirdSong();
	}

    private void Update()
    {
        if(_audioSource.clip==null)
            LoadBirdSong();
    }

    private void LoadBirdSong()
    {
        BirdSong = _birdDatabaseHandler.GetUnknownBirdSong();
        _audioSource.clip = BirdSong.Clip;
        _audioSource.Play();
    }

    private void Hit()
    {
        Debug.Log("Bird got hit. Starting identification..");

        _birdIdentificationScreen.StartBirdIdentification(this.gameObject);
        _player.SendMessage("HandleStateOutput", StateOutput.TransitionToIdentificationState);
    }
}