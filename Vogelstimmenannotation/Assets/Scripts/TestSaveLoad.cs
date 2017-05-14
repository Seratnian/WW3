using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveLoad : MonoBehaviour {

    private SaveToJSON<PlayerData> saveManager;

    private void Start()
    {
        saveManager = new SaveToJSON<PlayerData>();
        saveManager.SetSavePathAndName("D:/", "playerData.json");
        Debug.Log("SavePath: " + saveManager._fullPath);
    }

    public void TestFunctionality()
    {
        PlayerData original = new PlayerData();

        saveManager.Save(original);
        PlayerData loaded = saveManager.Load();

        //Debug.Log(string.Format("Original data: {0}\nLoaded data: {1}\nOriginal compared to loaded time stamp: {2}", original.TimeStamp, loaded.TimeStamp, System.DateTime.Compare(original.TimeStamp, loaded.TimeStamp)));
    }
}
