using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Respawner
{
    public class PlayerRespawner : MonoBehaviour
    {
        [SerializeField] List<Transform> _spawnPositions = new List<Transform>();
        [SerializeField] float _respawnDelay = 3.0f;
        [SerializeField] float counter = 0.0f;
        bool _isRespawning;
        
        private void Start()
        {
            PlayerEntity.StaticReference.OnPlayerDead.AddListener(HandlePlayerDeath);
        }

        private void HandlePlayerDeath()
        {
            if (!_isRespawning)
            {
                _isRespawning = true;
                StartCoroutine(HandlePlayerDeathRoutine());
            }
        }

        private IEnumerator HandlePlayerDeathRoutine()
        {
            while (counter < _respawnDelay)
            {
                counter += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            
            PlayerEntity.StaticReference.transform.position = _spawnPositions[UnityEngine.Random.Range(0,_spawnPositions.Count-1)].position;
            PlayerEntity.StaticReference.enabled = true;
            PlayerEntity.StaticReference.Respawn();

            counter = 0.0f;
            _isRespawning = false;
        }
    }
}

