using UnityEngine;
using Player.GroundScan;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

namespace Player
{
    [RequireComponent(typeof(PlayerInput),typeof(PlayerEntity))]
    public class PlayerControlls : MonoBehaviour
    {
        [SerializeField] float _jumpForce = 6.5f;
        [SerializeField] float _movementSpeed = 1.0f;

        PlayerEntity _playerEntity;
        Animator _animator;
        Rigidbody2D _rigidbody2D;

        Vector3 _initialScale;
        Vector2 _movementVector = Vector2.zero;

        PlayerGroundScan _groundScanner;

        private void Awake()
        {
            _initialScale = transform.localScale;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerEntity = GetComponent<PlayerEntity>();
            _groundScanner = transform.GetComponentInChildren<PlayerGroundScan>();
        }

        private void Start()
        {
            _playerEntity.PlayerDeadEvent.AddListener(OnPlayerDead);
            _playerEntity.PlayerRespawnEvent.AddListener(OnPlayerRespawn);
        }

        private void OnPlayerRespawn()
        {
            this.enabled = true;
        }

        private void OnPlayerDead()
        {
            this.enabled = false;
        }

        public void HandleMovementInput(InputAction.CallbackContext context)
        {
            _movementVector = context.ReadValue<Vector2>();
        }

        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            if (this.enabled)
            {
                if (_groundScanner.IsGrounded)
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
                }
            }
        }

        public void HandleAttackInput(InputAction.CallbackContext context)
        {
            if (this.enabled)
            {
                _animator.SetTrigger("Attacking");
            }
        }

        private void FixedUpdate()
        {
            ApplyMovementInput();
        }

        private void ApplyMovementInput()
        {
            if (_movementVector.x < 0)
            {
                _rigidbody2D.velocity = new Vector2(-3 * _movementSpeed, _rigidbody2D.velocity.y);
                transform.localScale = new Vector3(_initialScale.x, _initialScale.y, _initialScale.z);
            }
            else if (_movementVector.x > 0)
            {
                _rigidbody2D.velocity = new Vector2(3 * _movementSpeed, _rigidbody2D.velocity.y);
                transform.localScale = new Vector3(_initialScale.x * -1, _initialScale.y, _initialScale.z);
            }
        }
    }
}
