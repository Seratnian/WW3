using UnityEngine;
using System.Collections;

[System.Serializable]
public class Profile {
    public int Id { get; private set; }
    public string Name { get; private set; }
    public bool FinishedTutorial { get; private set; }

    public Profile(string name)
    {
        // TODO get number from Datebase
        Id = (int)(Random.value * 10000);
        // TODO
        Name = name;
        FinishedTutorial = false;
    }
}
