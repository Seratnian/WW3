using UnityEngine;
using System.IO;

public class SaveToJSON<T>
{

    public string SaveDataPath { get; private set; }
    public string SaveFileName { get; private set; }
    public string _fullPath { get { return string.Format("{0}/{1}", SaveDataPath, SaveFileName); } }

    public SaveToJSON()
    {
        SaveFileName = "savefile.json";
        SaveDataPath = string.Format("{0}/{1}", Application.persistentDataPath, SaveFileName);
    }

    public SaveToJSON(string path, string name)
    {
        SaveFileName = name;
        SaveDataPath = path;
    }

    public void Save(T saveFile)
    {
        string json = JsonUtility.ToJson(saveFile);
        File.WriteAllText(_fullPath, json);
    }

    public T Load()
    {
        string json = File.ReadAllText(_fullPath);
        T saveFile = JsonUtility.FromJson<T>(json);

        return saveFile;
    }

    public string ConvertToJSON(T saveFile)
    {
        return JsonUtility.ToJson(saveFile);
    }

    public void SetSavePathAndName(string path, string name)
    {
        SaveDataPath = path;
        SaveFileName = name;
    }
}