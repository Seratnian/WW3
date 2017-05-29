using UnityEngine;

[System.Serializable]
public class Profile
{
    public int Id { private set; get; }

    private string savePath;
    private PlayerData _playerData;
    private static LocalDataHandler<PlayerData> SaveDataHandler
    {
        get { return _saveDataHandler ?? (_saveDataHandler = new LocalDataHandlerImplementation<PlayerData>()); }
    }
    private static LocalDataHandler<PlayerData> _saveDataHandler;


    public Profile() : this("C:/WW3/")
    {
        
    }

    public Profile(string savePath)
    {
        this.savePath = savePath;
    }

    public void InitProfile()
    {
        Id = GetHashCode();
        Debug.Log("Profile Created. Id: " + Id);
        _playerData = new PlayerData();
    }

    public int GetId()
    {
        return Id;
    }

    public void Save()
    {
        SaveDataHandler.SetPathAndName(savePath, Id.ToString()+".playerData");
        SaveDataHandler.Save(_playerData);
    }

    public void Load()
    {
        SaveDataHandler.SetPathAndName(savePath, Id.ToString()+".playerData");
        _playerData = SaveDataHandler.Load();
    }
}