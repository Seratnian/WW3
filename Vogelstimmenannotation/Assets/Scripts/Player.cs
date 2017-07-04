using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public string Name;

    public static Player Instance { private set; get; }

    void Awake()
    {
        Debug.Assert(Instance==null, "Only one player should exist.");
        Instance = this;
    }

    void Start()
    {

    }
}
