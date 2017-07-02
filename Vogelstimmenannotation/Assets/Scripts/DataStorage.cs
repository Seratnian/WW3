using UnityEngine;
using System.Collections;

public class DataStorage : MonoBehaviour {
    public Profile Profile { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "RootScene":
                EventManager.StartListening("useProfile", StoreProfile);
                break;
        }
        
    }

    void StoreProfile(object data)
    {
        Profile = data as Profile;
        EventManager.StopListening("useProfile", StoreProfile);
    }

    void LogToConsole(object message)
    {
        Debug.Log(Profile.Name);
    }
}
