using Targetable;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(HitableEnemy), typeof(Animator), typeof(Rigidbody2D))]
    public class EnemyEntity : MonoBehaviour
    {
        [SerializeField] int _hitpoints;
        [SerializeField] bool _isAlive;
        Animator _animator;
        Vector3 _initScale;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _initScale = transform.localScale;
        }

        private void OnEnable()
        {
            _hitpoints = 100;
            _isAlive = true;
        }

        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                _hitpoints += value;
                Debug.Log(_hitpoints);
                if (_hitpoints <= 0)
                {
                    _isAlive = false;
                    _animator.SetBool("isDead", true);
                }
            }
        }

        public bool IsAlive { get => _isAlive; }

        void DisableEntity()
        {
            this.gameObject.SetActive(false);
        }

    }

}
