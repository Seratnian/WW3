using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadProgrammingScript : MonoBehaviour
{
    [SerializeField] private BirdDatabase m_birdDatabase;

    private static BirdDatabase _birdDatabase;

    private void Awake()
    {
        _birdDatabase = m_birdDatabase;
    }

    public static BirdDatabase GetBirdDatabaseInstance()
    {
        return _birdDatabase;
    }
}