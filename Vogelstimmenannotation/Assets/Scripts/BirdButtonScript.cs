using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdButtonScript : MonoBehaviour
{
    public InputField IdentifiedBird;
    public InputField ExcludedBird;

    public void WriteTextIntoIdentificationField()
    {
        IdentifiedBird.text = gameObject.name;
        ExcludedBird.text = gameObject.name;
    }
}