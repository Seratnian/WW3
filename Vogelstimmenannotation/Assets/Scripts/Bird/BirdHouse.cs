using System.Collections;
using NUnit.Framework;
using UnityEngine;

namespace WW3.GameWorld
{
    [RequireComponent(typeof(SphereCollider))]
    public class BirdHouse : MonoBehaviour, InteractableObject
    {
        private static BirdPool _birdPool;

        private Bird _currentBird;
        [SerializeField] private int _playerDetectionRange;

        public void Interact(object actor)
        {
            Interact();
        }

        public void Interact()
        {
            StoreBird();
            StartCoroutine(SpawnBird());
        }

        private void Awake()
        {
            GetComponent<SphereCollider>().radius = _playerDetectionRange;
            if (_birdPool == null) _birdPool = new BirdPool(2,"BirdPool_BirdHouses");
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