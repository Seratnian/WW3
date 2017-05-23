using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFactory : MonoBehaviour
{
    public GameObject BirdPrefab;
    public GameObject Player;
    private static GameObject _birdPrefab;

    private void Start()
    {
        _birdPrefab = BirdPrefab;
    }

    public static GameObject GetNewBird(Vector3 playerPosition, float distanceToPlayer)
    {
        playerPosition += (Random.onUnitSphere*distanceToPlayer);
        playerPosition.y = .5f;
        GameObject birdClone = GameObject.Instantiate(_birdPrefab);
        birdClone.transform.position = playerPosition;

        return birdClone;
    }

    public void TestAddBird()
    {
        GetNewBird(Player.transform.position, 3);
    }
}