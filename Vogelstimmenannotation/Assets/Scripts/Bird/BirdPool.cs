using System.Collections.Generic;
using UnityEngine;
using WW3.Utility;

namespace WW3.GameWorld
{
    public class BirdPool
    {
        private readonly GameObject _birdPool;
        private readonly GameObject _birdPrefab;
        private readonly Queue<Bird> _birds;

        public BirdPool()
        {
            _birds = new Queue<Bird>();
            PrefabCatalogue.Instance.MyDictionary.TryGetValue("Bird", out _birdPrefab);
            _birdPool = new GameObject("BirdPool");
            RefillThreshold = 5;
            FillQueueIfBelowThreshold();
        }

        public BirdPool(int refillThreshold)
        {
            _birds = new Queue<Bird>();
            PrefabCatalogue.Instance.MyDictionary.TryGetValue("Bird", out _birdPrefab);
            _birdPool = new GameObject("BirdPool");
            RefillThreshold = refillThreshold;
            FillQueueIfBelowThreshold();
        }

        public BirdPool(int refillThreshold, string gameObjectName)
        {
            _birds = new Queue<Bird>();
            PrefabCatalogue.Instance.MyDictionary.TryGetValue("Bird", out _birdPrefab);
            _birdPool = new GameObject(gameObjectName);
            RefillThreshold = refillThreshold;
            FillQueueIfBelowThreshold();
        }

        public int RefillThreshold { get; set; }

        public Bird GetBird()
        {
            Bird bird = _birds.Count < 1 ? CreateBird() : _birds.Dequeue();

            FillQueueIfBelowThreshold();

            return bird;
        }

        public void StoreBird(Bird bird)
        {
            bird.transform.SetParent(_birdPool.transform);

            _birds.Enqueue(bird);
        }

        private Bird CreateBird()
        {
            GameObject birdClone = Object.Instantiate(_birdPrefab);
            birdClone.transform.SetParent(_birdPool.transform);
            birdClone.transform.position = new Vector3(0,-100,0);

            Bird bird = birdClone.GetComponent<Bird>();
            bird.LoadSoundAndSetGameObjectTo(false);

            return bird;
        }

        private void FillQueueIfBelowThreshold()
        {
            if (_birds.Count >= RefillThreshold) return;

            for (int i = _birds.Count; i < RefillThreshold; i++)
                _birds.Enqueue(CreateBird());
        }
    }
}