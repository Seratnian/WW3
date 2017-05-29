using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileCreditsPage : MonoBehaviour
{
    [SerializeField] private GameObject profileCreditsPageContainer;

    public void ToggleProfileScreen()
    {
        profileCreditsPageContainer.SetActive(!profileCreditsPageContainer.activeSelf);
    }
}