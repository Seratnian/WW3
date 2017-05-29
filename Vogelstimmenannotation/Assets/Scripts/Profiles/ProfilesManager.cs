using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class ProfilesManager : MonoBehaviour
{
    
    private List<Profile> _profiles;
    private string _profilesDirectory, _profilesFileName;
    private string ProfilesFullPath { get { return _profilesDirectory + _profilesFileName; } }
    private Profile _currentProfile;

	private void Start ()
	{
	    _profilesDirectory = "C:/WW3/";
	    _profilesFileName = "profiles";

        if (!LoadProfiles())
	    {
            _profiles = new List<Profile>();
            _currentProfile = new Profile();
            _currentProfile.InitProfile();
            _profiles.Add(_currentProfile);
            Test();
	    }
        else
        {
            _currentProfile = _profiles.FirstOrDefault();
        }
	}

    public void Test()
    {
        if (LoadProfiles())
        {
            _currentProfile = _profiles.First();
            Debug.Log("Profile loaded. Id: "+_currentProfile.Id);
        }

        _currentProfile.Save();

        print("Savefile saved. Profile Id: "+_currentProfile.Id);

        SaveProfiles();
    }

    private void SaveProfiles()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(ProfilesFullPath);
        binaryFormatter.Serialize(fileStream, _profiles);
        fileStream.Close();
    }

    private bool LoadProfiles()
    {
        if (File.Exists(ProfilesFullPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(ProfilesFullPath, FileMode.Open);
            List<Profile> profiles = binaryFormatter.Deserialize(fileStream) as List<Profile>;
            fileStream.Close();

            _profiles = profiles;
            return true;
        }
        return false;
    }
    
}