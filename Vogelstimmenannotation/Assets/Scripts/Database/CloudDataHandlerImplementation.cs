using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDataHandlerImplementation : CloudDataHandler<PlayerData>
{
    private SaveToJSON<PlayerData> _localDataHandler;

    public bool CloudIsOlder(PlayerData localSave)
    {
        PlayerData cloudSave = Load();
        return cloudSave.Version < localSave.Version;
    }

    public PlayerData Load()
    {
        throw new NotImplementedException();
    }

    public void Save(PlayerData saveFile)
    {
        string json = _localDataHandler.ConvertToJSON(saveFile);

        //TODO: save json to cloud
        throw new NotImplementedException();
    }
}
