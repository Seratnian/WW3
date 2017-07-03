using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProfilesManager : MonoBehaviour
{
    public string SavePath = "./Profiles";

    private ArrayList Profiles;
    private BinaryFormatter formatter = new BinaryFormatter();
    private string AbsoluteSavePath;
    private string FileExtension = ".pro";

    void Start()
    {
        Profiles = new ArrayList();
        AbsoluteSavePath = new DirectoryInfo(SavePath).FullName;
        if (!Directory.Exists(AbsoluteSavePath))
        {
            Directory.CreateDirectory(AbsoluteSavePath);
        }
        LoadProfiles();
        EventManager.StartListening("createNewProfile", MakeProfile);
        ShowProfiles();
    }

    private void ShowProfiles()
    {
        foreach (Transform button in transform)
        {
            Destroy(button.gameObject);
        }
        Vector3 position = GameObject.Find("new Profile").GetComponent<ButtonPositionReset>().originalPosition;
        foreach (Profile profile in Profiles)
        {
            SendEventWhenPressed button = (Instantiate(Resources.Load("Button"), transform) as GameObject)
                .GetComponent("SendEventWhenPressed") as SendEventWhenPressed;
            position.Set(position.x, position.y + button.transform.lossyScale.y * 1.5f, position.z);
            button.transform.position = position;
            button.name = profile.Name;
            button.eventName = "useProfile";
            button.eventData = profile;
            TextMesh text = button.transform.GetChild(0).GetComponent<TextMesh>();
            text.text = profile.Name;
        }
    }

    void OnDestroy()
    {
        SaveProfiles();
    }

    public void MakeProfile(object data)
    {
        string name = "test_" + (int)(Random.value * 1000); // TODO get Name from DB
        Profile profile = new Profile(name);
        Profiles.Add(profile);
        SaveProfiles();
        ShowProfiles();
    }

    private bool LoadProfiles()
    {
        if (Directory.Exists(AbsoluteSavePath))
        {
            Profiles.Clear();
            foreach (string profilePath in Directory.GetFiles(AbsoluteSavePath))
            {
                FileStream stream = File.Open(profilePath, FileMode.Open);
                Profile profile = formatter.Deserialize(stream) as Profile;
                stream.Close();
                Profiles.Add(profile);
            }
            return true;
        }
        return false;
    }

    private bool SaveProfiles()
    {
        if (Directory.Exists(AbsoluteSavePath))
        {
            foreach (Profile profile in Profiles)
            {
                FileStream stream = File.Open(AbsoluteSavePath + @"\" + profile.Name + FileExtension, FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, profile);
                stream.Close();
            }
            return true;
        }
        return false;
    }
}
