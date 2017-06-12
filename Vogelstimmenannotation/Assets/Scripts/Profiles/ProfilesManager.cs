using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class ProfilesManager : MonoBehaviour
{
    public GameObject ProfilesContainer;
    public GameObject ButtonPrefab;
    public InputField CreateProfileName;
    public InputField DeleteProfileId;
    
    private List<Profile> _profiles;
    private string _profilesDirectory, _profilesFileName;
    private string ProfilesFullPath { get { return _profilesDirectory + _profilesFileName; } }
    private Profile _currentProfile;

	private void Start ()
	{
	    _profilesDirectory = "C:/WW3/";
	    _profilesFileName = "profiles";

	    LoadProfiles();
        CreateProfileButtons();
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

    private void CreateProfileButtons()
    {
        if(_profiles==null) return;

        ProfilesContainer.BroadcastMessage("Destroy", SendMessageOptions.DontRequireReceiver);
        foreach (Profile profile in _profiles)
        {
            GameObject button = Instantiate(ButtonPrefab);
            button.transform.SetParent(ProfilesContainer.transform);
            button.GetComponentInChildren<Text>().text = string.Format("{0}; ID: {1}", profile.Name, profile.Id);
            button.SetActive(true);
        }
    }

    public void CreateProfile()
    {
        Debug.Log("Creating new profile..");
        Profile profile = new Profile();

        if (_profiles == null)
            _profiles = new List<Profile>();                        

        profile.InitProfile(CreateProfileName.text);
        _profiles.Add(profile);
        SaveProfiles();
        CreateProfileButtons();
    }

    public void DeleteProfile()
    {
        foreach (Profile profile in _profiles)
        {
            if (profile.Id == Int32.Parse(DeleteProfileId.text))
                _profiles.Remove(profile);
        } 
        CreateProfileButtons();
    }

    public void SelectProfile(string buttonName)
    {
        foreach (Profile profile in _profiles)
        {
            if (string.Format("{0}; ID: {1}", profile.Name, profile.Id) == buttonName)
            {
                _currentProfile = profile;
                Debug.Log(string.Format("Profile selected.\n{0}; ID: {1}", profile.Name, profile.Id));
            }
        }
    }

    public void SaveGame()
    {
        _currentProfile.Save();
    }

    public void LoadGame()
    {
        _currentProfile.Load();
    }
}