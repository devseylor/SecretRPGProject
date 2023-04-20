using UnityEngine;

namespace RPG.Core
{
    public class PersistentObject : MonoBehaviour
    {
        [SerializeField] GameObject _persistentObjectPrefab;

        static bool _hasSpawn;

        private void Awake()
        {
            if (_hasSpawn) return;

            SpawnPersistentObject();

            _hasSpawn = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab); ;
            DontDestroyOnLoad(persistentObject);
        }
    }
}