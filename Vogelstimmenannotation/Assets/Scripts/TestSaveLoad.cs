using System.Collections;
using UnityEngine;

public class TestSaveLoad : MonoBehaviour {

    private LocalDataHandlerImplementation<PlayerData> _saveManager;

    private void Start()
    {
        _saveManager = new LocalDataHandlerImplementation<PlayerData>("D:/", "playerData.json");
        Debug.Log("SavePath: " + _saveManager.FullPath);
    }

    public void TestFunctionality()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        PlayerData original = new PlayerData();

        _saveManager.Save(original);
        PlayerData loaded = _saveManager.Load();

        yield return null;
        Debug.Log(string.Format("Original data: {0}\nLoaded data: {1}\nTimeDate.Compare(Saved, Loaded): {2}", original.TimeStamp, loaded.TimeStamp, System.DateTime.Compare(original.TimeStamp, loaded.TimeStamp)));
    }
}
