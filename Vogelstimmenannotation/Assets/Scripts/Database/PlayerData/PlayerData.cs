using System;
using UnityEngine;

[System.Serializable]
public class PlayerData
{  
    public DateTime TimeStamp;

    public PlayerData()
    {
        TimeStamp = DateTime.UtcNow;
    }
}
