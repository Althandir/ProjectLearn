using UnityEngine;
using UnityEngine.Events;

namespace Core.City
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CityGate : MonoBehaviour
    {
        static CityGate s_CityGate;
        public static CityGate StaticReference { get => s_CityGate; }

        UnityEvent _enemyEnteredGate = new UnityEvent();
        public UnityEvent EnemyEnteredGateEvent { get => _enemyEnteredGate;}

        

        #region Unity Messages
        private void Awake()
        {
            CreateSingleton();
        }

        private void OnDestroy()
        {
            DestroySingleton();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CheckForEnemy(collision);
        }

        #endregion

        #region EnemyEnteredGateHandling
        void CheckForEnemy(Collider2D collider)
        {
            if (collider.GetComponent<Enemy.EnemyEntity>())
            {
                DisableEnemyEntity(collider.gameObject);
                EnemyEnteredGate();
            }
        }

        void DisableEnemyEntity(GameObject enemy)
        {
            enemy.SetActive(false);
        }

        void EnemyEnteredGate()
        {
            Debug.Log("Enemy entered the Gate!");
            _enemyEnteredGate.Invoke();
        }

        
        #endregion

        #region SingletonHandling
        private void CreateSingleton()
        {
            if (!s_CityGate)
            {
                s_CityGate = this;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void DestroySingleton()
        {
            if (s_CityGate == this)
            {
                s_CityGate = null;
            }
        }
        #endregion
    }

}
