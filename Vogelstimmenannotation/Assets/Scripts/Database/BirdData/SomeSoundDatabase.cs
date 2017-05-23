using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SomeSoundDatabase : MonoBehaviour
{
    public AudioClip[] SoundFiles;

    private static SomeSoundDatabase _someSoundDatabaseInstance;

    private void Start()
    {
        _someSoundDatabaseInstance = this;        
    }

    public static SomeSoundDatabase GetSomeSoundDatabaseInstance()
    {
        return _someSoundDatabaseInstance;
    }

    public AudioClip GetRandomSound()
    {
        AudioClip file = SoundFiles[Mathf.RoundToInt(Random.value * SoundFiles.Length)];

        return file;
    }

    public string GetRandomAudioClipUrl()
    {
        return "http://upload.wikimedia.org/wikipedia/commons/a/a9/XN_Luscinia_megarhynchos_012.ogg";
    }
}