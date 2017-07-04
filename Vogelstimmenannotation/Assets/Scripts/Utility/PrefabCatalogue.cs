using System;
using System.Collections.Generic;
using UnityEngine;

namespace WW3.Utility
{
    [CreateAssetMenu(fileName = "PrefabCatalogue", menuName = "PrefabCatalogue")]
    public class PrefabCatalogue : ScriptableObject, ISerializationCallbackReceiver
    {
        public static PrefabCatalogue Instance { get; private set; }

        private void OnEnable()
        {
            Debug.Assert(Instance == null, "There should be only one PrefabCatalogue.");
            Instance = this;
        }

        #region UnitySerialization

        [SerializeField] private List<string> _keys = new List<string>();
        [SerializeField] private List<GameObject> _values = new List<GameObject>();

        //Unity doesn't know how to serialize a Dictionary
        public Dictionary<string, GameObject> MyDictionary = new Dictionary<string, GameObject>();

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();

            foreach (KeyValuePair<string, GameObject> kvp in MyDictionary)
            {
                _keys.Add(kvp.Key);
                _values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            MyDictionary = new Dictionary<string, GameObject>();

            for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
                MyDictionary.Add(_keys[i], _values[i]);
        }

        #endregion
    }
}