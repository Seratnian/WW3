using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void Test()
    {
        PlayerData data = new PlayerData();

        Debug.Log("Version: "+data.Version);
        Debug.Log("JSON :"+JsonUtility.ToJson(data));
    }
}
