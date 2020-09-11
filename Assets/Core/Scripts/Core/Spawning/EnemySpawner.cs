using Enemy;
using Enemy.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject _EnemyPrefab;
        [SerializeField] List<Transform> _spawnPostitions = new List<Transform>();
        [SerializeField] int _maxEnemyCount = 0;
        [SerializeField][Range(1.0f, 5.0f)] float _minSpawnDelaySec = 1.0f;
        [SerializeField][Range(1.0f, 5.0f)] float _maxSpawnDelaySec = 5.0f;
        [SerializeField] protected float _enemyCountMultiplicator = 1.5f;

        int _currentEnemyCount = 0;
        bool _activeSpawnerRoutine;

        UnityEvent _allChildrenDisabledEvent = new UnityEvent();

        public UnityEvent AllChildrenDisabledEvent { get => _allChildrenDisabledEvent; }

        private void Awake()
        {
            CheckForPrerequisites();
        }

        void CheckForPrerequisites()
        {
            if (!_EnemyPrefab)
            {
                Debug.LogError("No Prefab in Spawner! " + gameObject.name);
                this.enabled = false;
            }
            if (_spawnPostitions.Count == 0)
            {
                Debug.LogError("No Spawnpositions in Spawner! " + gameObject.name);
                this.enabled = false;
            }
            if (_minSpawnDelaySec > _maxSpawnDelaySec)
            {
                _minSpawnDelaySec = _maxSpawnDelaySec;
                Debug.LogError("MinSpawnDelay is bigger than MaxSpawnDelay! Always using MaxDelay.");
            }
        }

        /// <summary>
        /// Starts the Spawner, if the Routine is not already running and there are no active Childs. 
        /// </summary>
        [ContextMenu("DEBUG_StartSpawner")]
        public void StartSpawner()
        {
            if (!_activeSpawnerRoutine && !HasActiveChildren())
            {
                CreateNewChildren();
                StartCoroutine(SpawnRoutine());
            }
            else
            {
                Debug.LogWarning("Spawner or Children active!");
            }
        }

        /// <summary>
        /// Checks if the spawner has any active Enemies.
        /// </summary>
        /// <returns>True when any child is active, False when every child is inactive</returns>
        public bool HasActiveChildren()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }

        void CreateNewChildren()
        {
            while (transform.childCount != _maxEnemyCount)
            {
                GameObject newEnemy = Instantiate(_EnemyPrefab, Vector3.zero, Quaternion.identity, this.transform);
                newEnemy.GetComponent<EnemyEntity>().OnKilledEvent.AddListener(ChildDisabledHandler);
                Player.PlayerScore.Instance.AddListenerForEnemyDead(newEnemy.GetComponent<EnemyEntity>());
                newEnemy.SetActive(false);
            }
        }

        private void ChildDisabledHandler()
        {
            if (!HasActiveChildren())
            {
                _allChildrenDisabledEvent.Invoke();
            }
        }

        IEnumerator SpawnRoutine()
        {
            _activeSpawnerRoutine = true;
            while (_activeSpawnerRoutine && _currentEnemyCount < _maxEnemyCount)
            {
                foreach (Transform enemyEntity in this.transform)
                {
                    yield return new WaitForSecondsRealtime(Random.Range(_minSpawnDelaySec,_maxSpawnDelaySec));
                    MoveEnemyToSpawnPosPosition(enemyEntity);
                    enemyEntity.gameObject.SetActive(true);
                    enemyEntity.GetComponent<EnemyAI>().enabled = true;
                    _currentEnemyCount += 1;
                }
            }

            _currentEnemyCount = 0;
            _activeSpawnerRoutine = false;

            IncreaseMaxCounter();
        }

        void MoveEnemyToSpawnPosPosition(Transform enemyEntity)
        {
            enemyEntity.position = _spawnPostitions[UnityEngine.Random.Range(0, _spawnPostitions.Count)].position;
        }

        virtual protected void IncreaseMaxCounter()
        {
            _maxEnemyCount = Mathf.RoundToInt((float)_maxEnemyCount * _enemyCountMultiplicator);
        }
    }
}

