using UnityEngine;

namespace Core.TargetGate
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TargetGate : MonoBehaviour
    {
        static TargetGate s_TargetGate;

        public static TargetGate Reference { get => s_TargetGate; }

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

        void CheckForEnemy(Collider2D collider)
        {
            if (collider.GetComponent<Enemy.EnemyEntity>())
            {
                collider.gameObject.SetActive(false);
                EnemyEnteredGate();
            }
        }

        void EnemyEnteredGate()
        {
            Debug.Log("Enemy entered the Gate!");
        }

        #region SingletonHandling
        private void CreateSingleton()
        {
            if (!s_TargetGate)
            {
                s_TargetGate = this;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void DestroySingleton()
        {
            if (s_TargetGate == this.transform)
            {
                s_TargetGate = null;
            }
        }
        #endregion
    }

}
