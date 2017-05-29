#define USEBINARYFORMATTER

using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

public class LocalDataHandlerImplementation<T> : LocalDataHandler<T> where T : PlayerData
{
    public string SaveDataPath { get; private set; }
    public string SaveFileName { get; private set; }
    public string FullPath { get { return string.Format("{0}/{1}", SaveDataPath, SaveFileName); } }

    #region Constructor   
    public LocalDataHandlerImplementation()
    {
        SetPathAndName(Application.persistentDataPath, "savefile.json");
    }

    public LocalDataHandlerImplementation(string path, string name)
    {
        SetPathAndName(path, name);
        if (!Directory.Exists(SaveDataPath))
            Directory.CreateDirectory(SaveDataPath);
    }
    #endregion

    #region LocalDataHandler methods

    public void SetPathAndName(string path, string name)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        SaveDataPath = path;
        SaveFileName = name;
    }

#if USEBINARYFORMATTER
    public void Save(T saveFile)
    {
        SaveUsingBinary(saveFile);
    }
#elif !USEBINARYFORMATTER
    public void Save(T saveFile)
    {
        string json = JsonConvert.SerializeObject(saveFile);
        File.WriteAllText(FullPath, json);        
    }
#endif

    private void SaveUsingBinary(T savefile)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(FullPath);
        binaryFormatter.Serialize(fileStream, savefile);
        fileStream.Close();
    }
#if USEBINARYFORMATTER
    public T Load()
    {
        return LoadUsingBinary();
    }
#elif !USEBINARYFORMATTER
    public T Load()
    {        
        string json = File.ReadAllText(FullPath);
        T saveFile = JsonConvert.DeserializeObject<T>(json);

        return saveFile;
    }
#endif

    private T LoadUsingBinary()
    {
        if (File.Exists(FullPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(FullPath, FileMode.Open);
            T saveFile = binaryFormatter.Deserialize(fileStream) as T;
            fileStream.Close();

            return saveFile;
        }
        else throw new FileNotFoundException();
    }
#endregion
}