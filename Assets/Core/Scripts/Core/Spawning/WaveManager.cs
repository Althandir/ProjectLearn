using Core.EnemySpawner;
using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawner[] _spawners;
    [SerializeField] float _startDelaySec = 3.0f;
    [SerializeField] bool _SpawnWave;

    private void Awake()
    {
        _spawners = transform.GetComponentsInChildren<EnemySpawner>();
        AddListenersToSpawners();
    }

    public void Start()
    {
        StartNextWave();
    }

    private void OnDisable()
    {

    }

    void AddListenersToSpawners()
    {
        foreach (EnemySpawner spawner in _spawners)
        {
            spawner.AllChildrenDisabledEvent.AddListener(CheckForNextWave);
        }
    }

    void CheckForNextWave()
    {
        foreach (EnemySpawner spawner in _spawners)
        {
            if (spawner.HasActiveChildren())
            {
                return;
            }
        }

        StartNextWave();
    }

    void StartNextWave()
    {
        _SpawnWave = true;
        StartCoroutine(WaveRoutine());
    }

    IEnumerator WaveRoutine()
    {
        while (_SpawnWave)
        {
            yield return new WaitForSecondsRealtime(_startDelaySec);
            foreach (EnemySpawner spawner in _spawners)
            {
                spawner.StartSpawner();
            }
            _SpawnWave = false;
        }
    }

}
