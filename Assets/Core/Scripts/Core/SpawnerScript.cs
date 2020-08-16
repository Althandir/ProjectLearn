﻿using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Spawner
{
    public class SpawnerScript : MonoBehaviour
    {
        [SerializeField] GameObject _EnemyPrefab;
        [SerializeField] List<Transform> _spawnPostitions = new List<Transform>();
        [SerializeField] int _maxCounter = 0;
        [SerializeField] float _spawnDelayInSec = 1.0f;

        int _currentCounter = 0;
        bool _activeSpawnerRoutine;


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
        }

        private void Awake()
        {
            CheckForPrerequisites();
        }

        [ContextMenu("DEBUG_StartSpawner")]
        void StartSpawner()
        {
            if (!_activeSpawnerRoutine && NoActiveChildren())
            {
                CheckAmountOfChildren();
                StartCoroutine(SpawnRoutine());
            }
            else
            {
                Debug.LogWarning("Spawner or Children active!");
            }
        }

        private bool NoActiveChildren()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }

        void CheckAmountOfChildren()
        {
            while (transform.childCount != _maxCounter)
            {
                Debug.Log("Spawner need more Children! Generating...");
                GameObject newEnemy = Instantiate(_EnemyPrefab, Vector3.zero, Quaternion.identity, this.transform);
                newEnemy.SetActive(false);
            }
            Debug.Log("There are enough Children for the Spawner.");
        }

        IEnumerator SpawnRoutine()
        {
            _activeSpawnerRoutine = true;
            while (_activeSpawnerRoutine && _currentCounter < _maxCounter)
            {
                foreach (Transform enemyEntity in this.transform)
                {
                    yield return new WaitForSecondsRealtime(_spawnDelayInSec);
                    MoveEnemyToSpawnPosPosition(enemyEntity);
                    enemyEntity.gameObject.SetActive(true);
                    _currentCounter += 1;
                }
            }

            _currentCounter = 0;
            _activeSpawnerRoutine = false;
        }

        void MoveEnemyToSpawnPosPosition(Transform enemyEntity)
        {
            enemyEntity.position = _spawnPostitions[UnityEngine.Random.Range(0, _spawnPostitions.Count - 1)].position;
        }

    }
}

