using Core.AttackSystem;
using Player.GroundScan;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class PlayerEntity : Core.Entity
    {
        static PlayerEntity s_PlayerEntity;

        [SerializeField] int _damageValue = 25;

        [SerializeField] AttackArea _attackArea;
        [SerializeField] Cinemachine.CinemachineVirtualCamera _playerVirtualCamera;

        EventInt _onPlayerLifeChanged = new EventInt();
        UnityEvent _onPlayerDead = new UnityEvent();
        UnityEvent _onPlayerRespawn = new UnityEvent();

        PlayerGroundScan _groundScanner;

        Rigidbody2D _rigidbody2D;

        public UnityEvent PlayerDeadEvent { get => _onPlayerDead; }
        public int DamageValue { get => _damageValue; }

        public override int Hitpoints
        {
            get => _currentHitpoints;
            /*
            set
            {
                if (_currentHitpoints + value > MaxHitpoints)
                {
                    _currentHitpoints = MaxHitpoints;
                }
                else
                {
                    _currentHitpoints += value;
                }

                _onPlayerLifeChanged.Invoke(_currentHitpoints);
                if (_currentHitpoints <= 0)
                {
                    _onPlayerDead.Invoke();
                }
            }
            */
        }

        public static PlayerEntity Instance { get => s_PlayerEntity ;}
        public EventInt PlayerLifeChangedEvent { get => _onPlayerLifeChanged; }
        public int MaxHitpoints { get => _maxHitpoints; }
        public UnityEvent PlayerRespawnEvent { get => _onPlayerRespawn; }

        #region Hitpoint functions

        public override void EntityDeathHandler()
        {
            _onPlayerDead.Invoke();
        }

        public override void AddHitpoints(int value)
        {
            base.AddHitpoints(value);
            _onPlayerLifeChanged.Invoke(_currentHitpoints);
        }

        public override void DecreaseHitpoints(int value)
        {
            base.DecreaseHitpoints(value);
            _onPlayerLifeChanged.Invoke(_currentHitpoints);
        }

        public override void MultiplyHitpoints(float value)
        {
            base.MultiplyHitpoints(value);
            _onPlayerLifeChanged.Invoke(_currentHitpoints);
        }

        public override void DivideHitpoints(float value)
        {
            base.DivideHitpoints(value);
            _onPlayerLifeChanged.Invoke(_currentHitpoints);
        }

        public override void SetHitpoints(int value)
        {
            base.SetHitpoints(value);
            _onPlayerLifeChanged.Invoke(_currentHitpoints);
        }
        #endregion

        #region UnityMessages
        private void Awake()
        {
            CreateSingleton();

            _currentHitpoints = _maxHitpoints;

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
            _playerVirtualCamera.Priority = -1000;
            _onPlayerDead.Invoke();
        }
        #endregion

        #region RespawnHandling
        public void Respawn()
        {
            _onPlayerRespawn.Invoke();
            _animator.SetBool("isDead", false);
            SetHitpoints(_maxHitpoints);
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
            else if (_rigidbody2D.velocity.y < 0.1f && _groundScanner.IsGrounded)
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

