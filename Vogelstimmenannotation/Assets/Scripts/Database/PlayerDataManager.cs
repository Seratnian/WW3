using UnityEngine;

public enum PlayerDataSource { Cloud, Local}

public class PlayerDataManager : MonoBehaviour {

    [SerializeField]private PlayerData _playerData;
    private CloudDataHandler<PlayerData> _cloudDataHandler;
    private LocalDataHandler<PlayerData> _localDataHandler;

	void Start ()
    {
        _cloudDataHandler = new CloudDataHandlerImplementation<PlayerData>();
        _localDataHandler = new LocalDataHandlerImplementation<PlayerData>();
	}
	
    public void SaveDataOverrideOlder()
    {
        if(!_cloudDataHandler.CloudDataIsNewer(_playerData))                           
            _cloudDataHandler.Save(_playerData);
        if(!_localDataHandler.LocalDataIsNewer(_playerData))
            _localDataHandler.Save(_playerData);
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
        if(_cloudDataHandler.CloudDataIsNewer(_playerData))
            LoadData(PlayerDataSource.Cloud);
        if(_localDataHandler.LocalDataIsNewer(_playerData))
            LoadData(PlayerDataSource.Local);
        _cloudDataHandler.Save(_playerData);
        _localDataHandler.Save(_playerData);
    }
}