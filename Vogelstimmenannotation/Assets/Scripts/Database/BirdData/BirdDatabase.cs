using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

[CreateAssetMenu(fileName = "BirdDatabase", menuName = "BirdDatabase")]
[System.Serializable]
public class BirdDatabase : ScriptableObject, BirdDatabaseHandler
{
    [SerializeField]private BirdSong[] _birdSongs;

    public BirdSong GetUnknownBirdSong()
    {
        BirdSong unkownBird = _birdSongs.FirstOrDefault(x => x.Identified == false);
        
        return unkownBird;
    }

    public void IdentifyBird(BirdSong birdSong, string bird)
    {
        throw new System.NotImplementedException();
    }

    public void ExcludeBird(BirdSong birdSong, string[] birds)
    {
        throw new System.NotImplementedException();
    }
}