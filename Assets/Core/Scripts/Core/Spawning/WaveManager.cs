using System.Collections;
using UnityEngine;

namespace Core.Spawning
{
    public class WaveManager : MonoBehaviour
    {
        static WaveManager s_waveManager;

        [SerializeField] EnemySpawner[] _spawners;
        [SerializeField] float _startDelaySec = 3.0f;
        [SerializeField] bool _SpawningWave;
        [SerializeField] int _waveCounter = 1;

        EventInt _NextWaveEvent = new EventInt();

        public EventInt NextWaveEvent { get => _NextWaveEvent; }
        static public WaveManager Instance { get => s_waveManager; }
        public int WaveCounter { get => _waveCounter;}

        private void Awake()
        {
            CreateSingleton();
            _spawners = transform.GetComponentsInChildren<EnemySpawner>();
            AddListenersToSpawners();
        }

        public void Start()
        {
            StartNextWave();
        }

        private void OnDestroy()
        {
            DestroySingleton();
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
            _SpawningWave = true;
            StartCoroutine(WaveRoutine());
        }

        IEnumerator WaveRoutine()
        {
            while (_SpawningWave)
            {
                yield return new WaitForSecondsRealtime(_startDelaySec);

                _waveCounter += 1;
                _NextWaveEvent.Invoke(_waveCounter);

                foreach (EnemySpawner spawner in _spawners)
                {
                    spawner.StartSpawner();
                }
                _SpawningWave = false;
            }
        }

        #region Singleton
        void CreateSingleton()
        {
            if (!s_waveManager)
            {
                s_waveManager = this;
            }
            else
            {
                Debug.LogError($"Duplicated {this} found in {this.gameObject}!");
                Destroy(this);
            }
        }
        void DestroySingleton()
        {
            if (s_waveManager = this)
            {
                s_waveManager = null;
            }
        }

        #endregion

    }
}