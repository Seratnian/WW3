using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveScript : MonoBehaviour {

    private void OnMouseDown()
    {
        Debug.Log("Saving..");
        SaveData();
        UploadPlayerData();
    }

    private void SaveData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        float randomNumber = Random.value;
        string randomString = Random.value.ToString();

        Debug.Log(string.Format("Saved number: {0}/Saved string: {1}", randomNumber, randomString));

        binaryFormatter.Serialize(fileStream, new Save(randomNumber, randomString));

        fileStream.Close();
    }

    private void UploadPlayerData()
    {
        //sync with database
    }
}
