using Core.Attack;
using Player.GroundScan;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        static PlayerEntity s_PlayerEntity;

        [SerializeField] int _maxHitpoints = 100;
        [SerializeField] int _hitpoints; 

        [SerializeField] float _jumpForce = 0.0f;
        [SerializeField] float _movementSpeed = 1.0f;
        [SerializeField] int _damageValue = 25;

        [SerializeField] AttackArea _attackArea;

        bool _leftMovementActive;
        bool _rightMovementActive;
        bool _jumpPressed;
        Animator _animator;


        EventInt _onPlayerLifeChanged = new EventInt();
        UnityEvent _onPlayerDead = new UnityEvent();
        UnityEvent _onPlayerRespawn = new UnityEvent();

        PlayerGroundScan _groundScanner;

        Rigidbody2D _rigidbody2D;
        Vector3 _initialScale;


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
            _initialScale = transform.localScale;
            _onPlayerDead.AddListener(PlayerDead);
        }

        void Update()
        {
            ReadInput();
        }

        private void FixedUpdate()
        {
            RotateTowardsMovementDirection();
            ApplyInput();
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
        void PlayerDead()
        {
            _animator.SetBool("isDead", true);

            this.enabled = false;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.gravityScale = 0;
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

        #region Inputsystem
        private void ReadInput()
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            {
                _leftMovementActive = true;
            }
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            {
                _rightMovementActive = true;
            }
            if (Keyboard.current.spaceKey.isPressed && _groundScanner.IsGrounded)
            {
                _jumpPressed = true;
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _animator.SetTrigger("Attacking");
            }
        }

        private void ApplyInput()
        {
            // Movement
            // When both Keys are pressed
            if (_leftMovementActive && _rightMovementActive)
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                _leftMovementActive = false;
                _rightMovementActive = false;
            }
            else
            {
                if (_leftMovementActive)
                {
                    _rigidbody2D.velocity = new Vector2(-3 * _movementSpeed, _rigidbody2D.velocity.y);
                    _leftMovementActive = false;
                }
                if (_rightMovementActive)
                {
                    _rigidbody2D.velocity = new Vector2(3 * _movementSpeed, _rigidbody2D.velocity.y);
                    _rightMovementActive = false;
                }
            }
            
            // Jumping
            if (_jumpPressed)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
                _jumpPressed = false;
            }
        }

        void RotateTowardsMovementDirection()
        {
            if (_leftMovementActive)
            {
                transform.localScale = new Vector3(_initialScale.x, _initialScale.y, _initialScale.z);
            }
            else if (_rightMovementActive)
            {
                transform.localScale = new Vector3(_initialScale.x * -1, _initialScale.y, _initialScale.z);
            }
        }
        #endregion
    }
}

