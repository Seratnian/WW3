using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadScript : MonoBehaviour {

    private void OnMouseDown()
    {
        Debug.Log("Loading..");
        DownloadPlayerData();
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            Save playerData = (Save) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            float randomNumber = playerData.someNumber;
            string randomString = playerData.someString;

            Debug.Log(string.Format("Loaded number: {0}/nLoaded string: {1}", randomNumber, randomString));
        }
    }

    private void DownloadPlayerData()
    {
        
    }
}
