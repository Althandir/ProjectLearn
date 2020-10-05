using Targetable;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    [RequireComponent(typeof(HitboxEnemy), typeof(Animator), typeof(Rigidbody2D))]
    public class EnemyEntity : MonoBehaviour
    {
        [SerializeField] int _maxHitpoints;

        [SerializeField] int _currentHitpoints;
        [SerializeField] bool _isAlive;
        Animator _animator;
        Vector3 _initScale;

        UnityEvent _OnKilledEvent = new UnityEvent();

        public int Hitpoints
        {
            get => _currentHitpoints;
            set
            {
                _currentHitpoints += value;
                if (_currentHitpoints <= 0)
                {
                    _isAlive = false;
                    _animator.SetBool("isDead", true);
                }
            }
        }

        public bool IsAlive { get => _isAlive; }
        public UnityEvent OnKilledEvent { get => _OnKilledEvent; }

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

        public void DisableEntity()
        {
            this.gameObject.SetActive(false);
            _OnKilledEvent.Invoke();
        }
    }
}
