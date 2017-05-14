using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SavePolicy { OverrideOlder, OverrideAll}
public enum PlayerDataSource { Cloud, Local}

public class PlayerDataManager : MonoBehaviour {

    [SerializeField]private PlayerData _playerData;
    private CloudDataHandler<PlayerData> _cloudDataHandler;
    private SaveToJSON<PlayerData> _localDataHandler;
    private PlayerData CurrentLocalSave { get { return _localDataHandler.Load(); } }
    private PlayerData CurrentCloudSave { get { return _cloudDataHandler.Load(); } }

	void Start () {
        _cloudDataHandler = new CloudDataHandlerImplementation();
        _localDataHandler = new SaveToJSON<PlayerData>();
	}
	
    public void SaveData(SavePolicy policy)
    {
        if(policy==SavePolicy.OverrideAll)
        {
            _localDataHandler.Save(_playerData);
            _cloudDataHandler.Save(_playerData);
        }
        if(policy==SavePolicy.OverrideOlder)
        {
            if(_cloudDataHandler.CloudIsOlder(CurrentLocalSave))
            {
                _localDataHandler.Save(_playerData);
                _cloudDataHandler.Save(_playerData);
            }
        }
    }

    public void LoadData(PlayerDataSource source)
    {
        if(source == PlayerDataSource.Cloud)
        {
            _playerData = _cloudDataHandler.Load();
        }
        if(source == PlayerDataSource.Local)
        {
            _playerData = _localDataHandler.Load();
        }
    }

    public void UpdateEverythingToLatestVersion()
    {
        if(_cloudDataHandler.CloudIsOlder(CurrentLocalSave))
        {
            _cloudDataHandler.Save(CurrentLocalSave);
        }
        else
        {
            _localDataHandler.Save(CurrentCloudSave);
        }
    }
}