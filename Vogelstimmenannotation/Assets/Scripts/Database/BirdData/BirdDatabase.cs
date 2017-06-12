using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "BirdDatabase", menuName = "BirdDatabase")]
[System.Serializable]
public class BirdDatabase : ScriptableObject, BirdDatabaseHandler
{
    [SerializeField]private BirdSong[] _birdSongs;

    public BirdSong GetUnknownBirdSong()
    {
        BirdSong[] songs = _birdSongs.Where(x => x.Identified == false).ToArray();
        BirdSong unkownBird = songs[UnityEngine.Random.Range(0, songs.Length)];
        
        return unkownBird;
    }

    public void IdentifyBird(BirdSong birdSong, string bird)
    {
        birdSong.IsBird = bird;
    }

    public void ExcludeBird(BirdSong birdSong, string bird)
    {
        List<string> list = birdSong.IsNotBird.ToList();
        list.Add(bird);
        birdSong.IsNotBird = list.ToArray();
    }
}