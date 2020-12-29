using Targetable;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(HitboxEnemy), typeof(Animator), typeof(Rigidbody2D))]
    public class EnemyEntity : Core.Entity
    {
        [SerializeField] bool _isAlive;
        Vector3 _initScale;

        UnityEvent _OnKilledEvent = new UnityEvent();

        public override int Hitpoints
        {
            get => _currentHitpoints;
            /*
            set
            {
                if (_currentHitpoints + value > _maxHitpoints)
                {
                    _currentHitpoints = _maxHitpoints;
                }
                else
                {
                    _currentHitpoints += value;
                }

                if (_currentHitpoints <= 0)
                {
                    _isAlive = false;
                    _animator.SetBool("isDead", true);
                }
            }
            */
        }

        public bool IsAlive { get => _isAlive; }
        public UnityEvent OnKilledEvent { get => _OnKilledEvent; }

        public override void EntityDeathHandler()
        {
            _isAlive = false;
            _animator.SetBool("isDead", true);
        }
        
        public void DisableEntity()
        {
            this.gameObject.SetActive(false);
            _OnKilledEvent.Invoke();
        }

        #region Unity Messages
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _initScale = transform.localScale;
        }
        private void OnEnable()
        {
            _currentHitpoints = _maxHitpoints;
            _isAlive = true;
        }
        #endregion

    }
}
