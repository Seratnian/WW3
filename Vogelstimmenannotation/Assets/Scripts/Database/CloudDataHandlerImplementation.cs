using System;
using UnityEngine;

public class CloudDataHandlerImplementation<T> : CloudDataHandler<T> where T:PlayerData
{
    public bool CloudDataIsNewer(T localSave)
    {
        throw new NotImplementedException();
    }

    public T Load()
    {
        throw new NotImplementedException();
    }

    public void Save(T saveFile)
    {
        string json = JsonUtility.ToJson(saveFile);

        //TODO: save json to cloud
        throw new NotImplementedException();
    }
}
