using Core.City;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Respawner
{
    public class PlayerRespawner : MonoBehaviour
    {
        [SerializeField] List<Transform> _spawnPositions = new List<Transform>();
        [SerializeField] float _respawnDelay = 3.0f;
        [SerializeField] float _timeCounter = 0.0f;
        bool _isRespawning;
        bool _isAllowedToRespawn = true;
        private void Start()
        {
            PlayerEntity.Instance.PlayerDeadEvent.AddListener(HandlePlayerDeath);
            CityValues.Instance.CityDestroyedEvent.AddListener(OnCityDeath);
        }

        private void OnCityDeath()
        {
            this.enabled = false;
            _isAllowedToRespawn = false;
            PlayerEntity.Instance.PlayerDeadEvent.RemoveListener(HandlePlayerDeath);
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
            while (_timeCounter < _respawnDelay)
            {
                _timeCounter += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            if (_isAllowedToRespawn)
            {
                PlayerEntity.Instance.transform.position = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Count - 1)].position;
                PlayerEntity.Instance.Respawn();
                PlayerEntity.Instance.enabled = true;

                _timeCounter = 0.0f;
                _isRespawning = false;
            }
        }
    }
}

