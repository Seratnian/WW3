using UnityEngine;

namespace WW3.GameWorld
{
    [RequireComponent(typeof(SphereCollider))]
    public class BirdSpawn : MonoBehaviour
    {
        private static BirdPool _birdPool;

        private Bird _currentBird;
        public int PlayerDetectionRange;

        private void Start()
        {
            GetComponent<SphereCollider>().radius = PlayerDetectionRange;
            if (_birdPool == null) _birdPool = new BirdPool();
        }

        private void OnTriggerEnter(Collider other)
        {
            SpawnBird();
        }

        private void OnTriggerExit(Collider other)
        {
            StoreBird();
        }

        private void SpawnBird()
        {
            if (_currentBird != null) return;

            _currentBird = _birdPool.GetBird();
            _currentBird.transform.SetParent(transform);
            _currentBird.transform.position = transform.position;
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

        public void TestSpawnBird()
        {
            SpawnBird();
        }

        public void TestStoreBird()
        {
            StoreBird();
        }
    }
}