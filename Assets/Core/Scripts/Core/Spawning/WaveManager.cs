using Core.EnemySpawner;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawner[] _spawners;

    private void Awake()
    {
        _spawners = transform.GetComponentsInChildren<EnemySpawner>();
    }


    private void OnEnable()
    {
        
    }
}
