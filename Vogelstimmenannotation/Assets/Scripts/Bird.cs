using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class Bird : MonoBehaviour
{
    [SerializeField]private AudioSource _audioSource;
    private static SomeSoundDatabase _someSoundDatabase;

	private void Start ()
	{
	    if (_someSoundDatabase == null) _someSoundDatabase = SomeSoundDatabase.GetSomeSoundDatabaseInstance();
	    
	    StartCoroutine(LoadBirdSong());
	}

    private void Update()
    {
        if(!_audioSource.isPlaying)
            _audioSource.Play();
    }

    private IEnumerator LoadBirdSong()
    {
        var clip = new WWW(_someSoundDatabase.GetRandomAudioClipUrl()).GetAudioClip(true, true);
        yield return null;
        
        while (clip.loadState != AudioDataLoadState.Loaded && clip.loadState != AudioDataLoadState.Failed)
        {
            Debug.Log(clip.loadState);
            yield return new WaitForEndOfFrame();
        }

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private void Hit()
    {
        Debug.Log("Bird got hit.");
    }
}