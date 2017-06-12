using UnityEngine;

[System.Serializable]
public class Profile
{
    public int Id { private set; get; }
    public string Name { private set; get; }

    private string _savePath;
    private PlayerData _playerData;
    private static LocalDataHandler<PlayerData> SaveDataHandler
    {
        get { return _saveDataHandler ?? (_saveDataHandler = new LocalDataHandlerImplementation<PlayerData>()); }
    }
    private static LocalDataHandler<PlayerData> _saveDataHandler;


    public Profile()
    {
        _savePath = "C:/WW3/";
    }

    public void InitProfile(string name)
    {
        Id = GetHashCode() / 100000;
        Name = name;
        if (Name == string.Empty) Name = "Unnamed";
        _playerData = new PlayerData();

        Save();
        Debug.Log(string.Format("Profile created. ID: {0}\nName: {1}", Id, name));
    }

    public void Save()
    {
        SaveDataHandler.SetPathAndName(_savePath, string.Format("{0}_{1}.playerData", Id, Name));
        SaveDataHandler.Save(_playerData);
    }

    public void Load()
    {
        SaveDataHandler.SetPathAndName(_savePath, string.Format("{0}_{1}.playerData", Id, Name));
        _playerData = SaveDataHandler.Load();
    }
}