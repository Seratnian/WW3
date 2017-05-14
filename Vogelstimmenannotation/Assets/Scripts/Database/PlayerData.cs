using System;
using UnityEngine;

public class PlayerData {

    //Unity's JsonUtility can't serialize DateTime fields
    //so we just increase some int by 1 everytime we save

    public int Version;

    public void IncreaseVersion()
    {
        Version++;
    }
}
