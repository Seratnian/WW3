using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BirdSong
{
    public AudioClip Clip { get { return _clip; } }
    [SerializeField] private AudioClip _clip;
    public string Id { get { return Clip.GetHashCode().ToString(); } } //use clip name?
    public string IsBird;
    public string[] IsNotBird;
    public bool Identified { get { return IsBird != ""; } }

    public BirdSong(AudioClip clip)
    {
        _clip = clip;
    }
}