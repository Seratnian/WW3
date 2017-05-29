using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BirdDatabaseHandler
{
    BirdSong GetUnknownBirdSong();
    void IdentifyBird(BirdSong birdSong, string bird);
    void ExcludeBird(BirdSong birdSong, string[] birds);
}