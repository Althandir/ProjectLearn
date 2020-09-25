using Core.AttackSystem;
using Player.GroundScan;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        static PlayerEntity s_PlayerEntity;

        [SerializeField] int _maxHitpoints = 100;
        [SerializeField] int _hitpoints; 

        [SerializeField] int _damageValue = 25;

        [SerializeField] AttackArea _attackArea;
        [SerializeField] Cinemachine.CinemachineVirtualCamera _playerVirtualCamera;

        Animator _animator;


        EventInt _onPlayerLifeChanged = new EventInt();
        UnityEvent _onPlayerDead = new UnityEvent();
        UnityEvent _onPlayerRespawn = new UnityEvent();

        PlayerGroundScan _groundScanner;

        Rigidbody2D _rigidbody2D;



        public UnityEvent OnPlayerDead { get => _onPlayerDead; }
        public int DamageValue { get => _damageValue; }

        public int Hitpoints
        {
            get => _hitpoints;
            set
            {
                _hitpoints += value;
                _onPlayerLifeChanged.Invoke(_hitpoints);
                if (_hitpoints <= 0)
                {
                    _onPlayerDead.Invoke();
                }
            }
        }

        public static PlayerEntity Instance { get => s_PlayerEntity ;}
        public EventInt OnPlayerLifeChanged { get => _onPlayerLifeChanged; }
        public int MaxHitpoints { get => _maxHitpoints; }
        public UnityEvent OnPlayerRespawn { get => _onPlayerRespawn; }


        #region UnityMessages
        private void Awake()
        {
            CreateSingleton();

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _groundScanner = transform.GetComponentInChildren<PlayerGroundScan>();
            _attackArea = transform.GetComponentInChildren<AttackArea>();
            _animator = GetComponent<Animator>();

            _onPlayerDead.AddListener(PlayerDeadHandler);
        }

        private void Start()
        {
            Core.City.CityValues.Instance.CityDestroyedEvent.AddListener(CityDeathHandler);
        }

        void Update()
        {

        }

        private void FixedUpdate()
        {
            ApplyAnimations();
        }

        private void OnDestroy()
        {
            DestroySingleton();
        }
        #endregion

        #region SingletonHandling
        private void CreateSingleton()
        {
            if (!s_PlayerEntity)
            {
                s_PlayerEntity = this;
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void DestroySingleton()
        {
            if (s_PlayerEntity == this.transform)
            {
                s_PlayerEntity = null;
            }
        }
        #endregion

        #region DeathHandling
        void PlayerDeadHandler()
        {
            _animator.SetBool("isDead", true);

            this.enabled = false;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.gravityScale = 0;
        }

        void CityDeathHandler()
        {
            PlayerDeadHandler();
            
            _playerVirtualCamera.Priority = -1000;
        }
        #endregion

        #region RespawnHandling
        public void Respawn()
        {
            _onPlayerRespawn.Invoke();
            _animator.SetBool("isDead", false);
            Hitpoints = _maxHitpoints;
            _rigidbody2D.gravityScale = 1;
        }
        #endregion

        #region AnimationManagement
        private void ApplyAnimations()
        {
            // Handle Moving
            if (Mathf.Abs(_rigidbody2D.velocity.x) > 0.25f)
            {
                if (!_animator.GetBool("isMoving"))
                {
                    _animator.SetBool("isMoving", true);
                }
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }

            // Handle Jumping
            if (Mathf.Abs(_rigidbody2D.velocity.y) > 0 && !_groundScanner.IsGrounded)
            {
                if (!_animator.GetBool("isInAir"))
                {
                    _animator.SetBool("isInAir", true);
                }
            }
            else if (Mathf.Abs(_rigidbody2D.velocity.y) < 0.05f && _groundScanner.IsGrounded)
            {
                _animator.SetBool("isInAir", false);
            }
        }

        //Required for Animator to be called
        private void OnAttack()
        {
            _attackArea.OnAttack(_damageValue);
        }
        #endregion

      
    }
}

