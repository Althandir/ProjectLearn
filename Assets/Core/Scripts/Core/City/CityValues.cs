using UnityEngine;
using UnityEngine.Events;

namespace Core.City
{
    public class CityValues : MonoBehaviour
    {
        static CityValues s_CityValues;

        [SerializeField] int _maxCityLife = 20;
        
        int _cityLife;

        UnityEvent _cityDestroyedEvent = new UnityEvent();
        UnityEventInt _cityLifeChangedEvent = new UnityEventInt();

        public UnityEvent CityDestroyedEvent { get => _cityDestroyedEvent; }
        public UnityEventInt CityLifeChangedEvent { get => _cityLifeChangedEvent; }
        public static CityValues StaticReference { get => s_CityValues; }
        public int MaxCityLife { get => _maxCityLife; }


        #region Unity Messages
        private void Awake()
        {
            CreateSingleton();

            _cityLife = _maxCityLife;
        }

        private void OnEnable()
        {
            CityGate.StaticReference.EnemyEnteredGateEvent.AddListener(OnEnemyEntered);
        }

        private void Start()
        {
            _cityLifeChangedEvent.Invoke(_cityLife);
        }

        private void OnDestroy()
        {
            DestroySingleton();
        }
        #endregion

        #region SingletonHandling
        private void CreateSingleton()
        {
            if (!s_CityValues)
            {
                s_CityValues = this;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void DestroySingleton()
        {
            if (s_CityValues == this)
            {
                s_CityValues = null;
            }
        }
        #endregion


        private void OnEnemyEntered()
        {
            DecreaseCityLife();
        }

        void DecreaseCityLife()
        {
            _cityLife -= 1;
            _cityLifeChangedEvent.Invoke(_cityLife);
            CheckCityLife();
        }

        void CheckCityLife()
        {
            if (_cityLife <= 0)
            {
                _cityDestroyedEvent.Invoke();
                CityGate.StaticReference.EnemyEnteredGateEvent.RemoveListener(OnEnemyEntered);
            }
        }
    }

}
