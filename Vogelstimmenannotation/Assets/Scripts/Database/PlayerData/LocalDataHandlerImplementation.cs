using System;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class LocalDataHandlerImplementation<T> : LocalDataHandler<T> where T:PlayerData
{

    public string SaveDataPath { get; private set; }
    public string SaveFileName { get; private set; }
    public string FullPath { get { return string.Format("{0}/{1}", SaveDataPath, SaveFileName); } }

#region Constructor   
    public LocalDataHandlerImplementation()
    {
        SetSavePathAndName(Application.persistentDataPath, "savefile.json");
    }

    public LocalDataHandlerImplementation(string path, string name)
    {
        SetSavePathAndName(path, name);
    }

    private void SetSavePathAndName(string path, string name)
    {
        SaveDataPath = path;
        SaveFileName = name;
    }
#endregion

#region LocalDataHandler methods
    public bool LocalDataIsNewer(T data)
    {
        return Load().TimeStamp.CompareTo(data.TimeStamp) == -1;
    }

    public void Save(T saveFile)
    {
        string json = JsonConvert.SerializeObject(saveFile);
        File.WriteAllText(FullPath, json);
    }

    public T Load()
    {
        string json = File.ReadAllText(FullPath);
        T saveFile = JsonConvert.DeserializeObject<T>(json);

        return saveFile;
    }
#endregion
}