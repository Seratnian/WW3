using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    public ProfilesManager ProfilesManager;
    public Text ButtonText;

    public void SelectProfile()
    {
        Debug.Log("Selecting profile..");
        ProfilesManager.SelectProfile(ButtonText.text);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}