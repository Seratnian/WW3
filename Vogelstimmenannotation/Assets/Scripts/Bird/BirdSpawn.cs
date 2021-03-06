﻿using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;

namespace WW3.GameWorld
{
    [RequireComponent(typeof(SphereCollider))]
    public class BirdSpawn : MonoBehaviour
    {
        private static BirdPool _birdPool;

        private Bird _currentBird;
        [SerializeField]private int _playerDetectionRange;

        private void Start()
        {
            GetComponent<SphereCollider>().radius = _playerDetectionRange;
            if (_birdPool == null) _birdPool = new BirdPool(5, "BirdPool_SpawnPoints");
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SpawnBird());
        }

        private void OnTriggerExit(Collider other)
        {
            StoreBird();
        }

        private IEnumerator SpawnBird()
        {
            if (_currentBird != null) yield break;

            _currentBird = _birdPool.GetBird();
            _currentBird.transform.SetParent(transform);
            _currentBird.transform.position = transform.position;
            while (!_currentBird.Ready)
                yield return new WaitForEndOfFrame();
            _currentBird.gameObject.SetActive(true);
            _currentBird.PlaySound(true);
        }

        private void StoreBird()
        {
            if (_currentBird == null) return;

            _currentBird.PlaySound(false);
            _currentBird.LoadSoundAndSetGameObjectTo(false);
            _birdPool.StoreBird(_currentBird);
            _currentBird = null;
        }
    }
}