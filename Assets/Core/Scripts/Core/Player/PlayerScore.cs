using System;
using UnityEngine;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        static PlayerScore s_PlayerScore;

        long score;
        [SerializeField] int _enemyPointValue = 10;

        EventLong _OnScoreChanged = new EventLong();

        static public PlayerScore Instance { get => s_PlayerScore; }
        public long Score 
        { 
            get => score;
            set 
            { 
                score = value;
                // TODO: maybe change to ulong?
                if (score < 0)
                {
                    score = 0;
                }
                _OnScoreChanged.Invoke(score);
            } 
        }

        public EventLong OnScoreChanged { get => _OnScoreChanged; }

        #region Unity Messages
        void Awake()
        {
            CreateSingleton();
        }

        private void Start()
        {
            LinkToCityValues();
            LinkToCityGate();
            LinkToWaveManager();
        }

        private void OnDestroy()
        {
            DestroySingleton();
        }

        #endregion

        #region RegisterEnemy
        public void AddListenerForEnemyDead(Enemy.EnemyEntity newEnemy)
        {
            newEnemy.OnKilledEvent.AddListener(OnEnemyDisable);
        }
        #endregion

        #region LinksToOtherObjects
        void LinkToCityValues()
        {
            Core.City.CityValues.Instance.CityDestroyedEvent.AddListener(HandleCityDeath);
        }

        void LinkToCityGate()
        {
            Core.City.CityGate.Instance.EnemyEnteredGateEvent.AddListener(HandleEnemyEnteredGate);
        }
        void LinkToWaveManager()
        {
            Core.Spawning.WaveManager.Instance.NextWaveEvent.AddListener(HandleNextWave);
        }

        #endregion

        #region EventHandling
        private void OnEnemyDisable()
        {
            Score += _enemyPointValue;
        }

        private void HandleEnemyEnteredGate()
        {
            Score -= _enemyPointValue * 2;
        }
        private void HandleNextWave(int waveCounter)
        {
            CalcNewEnemyPointValue(waveCounter);
        }

        private void HandleCityDeath()
        {
            Core.City.CityGate.Instance.EnemyEnteredGateEvent.RemoveListener(HandleEnemyEnteredGate);
            Core.Spawning.WaveManager.Instance.NextWaveEvent.RemoveListener(HandleNextWave);
        }
        #endregion

        private void CalcNewEnemyPointValue(int waveCounter)
        {
            _enemyPointValue += waveCounter * 10;
        }

        #region Singleton
        void CreateSingleton()
        {
            if (!s_PlayerScore)
            {
                s_PlayerScore = this;
            }
            else
            {
                Debug.LogError($"Duplicated {this} found in {this.gameObject}!");
                Destroy(this);
            }
        }

        void DestroySingleton()
        {
            if (s_PlayerScore = this)
            {
                s_PlayerScore = null;
            }
        }
        #endregion
    }
}
