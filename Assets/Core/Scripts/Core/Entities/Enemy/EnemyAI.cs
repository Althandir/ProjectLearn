using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy.AI
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] float _movementSpeed = 1.0f;
        [SerializeField] float _jumpCooldown = 3.0f;
        [SerializeField] float _jumpPower = 5;
        [SerializeField] float _jumpTriggerDistance = 2;
        [SerializeField] int _damageValue = 20;

        float _jumpTimer = 0.0f;

        Vector3 _initScale;
        Animator _animator;
        Rigidbody2D _rb2D;

        EnemyEntity _entity;
        Attack.EnemyAttackArea _attackArea;
        Targeting.EnemyTargeting _targetingSystem;

        bool _xMovementActive;
        bool _canJump;

        UnityEvent _StartTrackingPlayerEvent = new UnityEvent();

        public bool CanJump { get => _canJump; set => _canJump = value; }

        #region Unity
        private void Awake()
        {
            _initScale = transform.localScale;
            _animator = GetComponent<Animator>();
            _entity = GetComponent<EnemyEntity>();
            _rb2D = GetComponent<Rigidbody2D>();

            LinkToTargetingSystem();
            LinkToAttackArea();
        }

        private void LinkToTargetingSystem()
        {
            _targetingSystem = GetComponentInChildren<Targeting.EnemyTargeting>();
        }

        private void OnEnable()
        {
            StartXAxisMovement();
            StartJumpRoutine();
        }


        private void OnDisable()
        {
            StopXAxisMovement();
            StopJumpRoutine();
        }
        #endregion

        #region DeathHandling
        void DisableAI()
        {
            this.enabled = false;
        }
        #endregion

        #region AttackHandling
        void LinkToAttackArea()
        {
            _attackArea = GetComponentInChildren<Enemy.Attack.EnemyAttackArea>();
            _attackArea.PlayerDetectedEvent.AddListener(PlayerInRangeDetected);
        }

        #region PlayerTracking
        void PlayerInRangeDetected()
        {
            StartCoroutine(PlayerInAttackRangeRoutine());
            _StartTrackingPlayerEvent.Invoke();
        }

        IEnumerator PlayerInAttackRangeRoutine()
        {
            while (_attackArea.PlayerDetected && _entity.IsAlive)
            {
                _animator.SetTrigger("Attack");
                yield return new WaitForFixedUpdate();
            }
        }


        #endregion

        #region Attack Animator Methods
        void OnAttackStart()
        {
            StopXAxisMovement();
            StopJumpRoutine();
        }

        void OnAttackComplete()
        {
            _attackArea.OnAttack(_damageValue);
        }

        void OnAttackEnd()
        {
            StartXAxisMovement();
            StartJumpRoutine();
        }
        #endregion

        #endregion

        #region Movement
        private IEnumerator XAxisMovementRoutine()
        {
            while (_xMovementActive && _entity.IsAlive)
            {
                XAxisMovement();
                yield return new WaitForFixedUpdate();
            }
        }

        
        private IEnumerator JumpRoutine()
        {
            while(_entity.IsAlive)
            {
                _jumpTimer += Time.fixedDeltaTime;
                if (_jumpTimer > _jumpCooldown && _canJump && JumpRequired())
                {
                    _jumpTimer = 0.0f;
                    Jump();
                }

                InAirAnimationCheck();
                yield return new WaitForFixedUpdate();
            }
        }

        bool JumpRequired()
        {
            if (Mathf.Abs(_targetingSystem.TargetTransform.position.y - transform.position.y) > _jumpTriggerDistance)
            {
                return true;
            }
            return false;
        }

        private void InAirAnimationCheck()
        {
            if (Mathf.Abs(_rb2D.velocity.y) > 1f)
            {
                if (!_animator.GetBool("isInAir"))
                {
                    _animator.SetBool("isInAir", true);
                }
            }
            else
            {
                if (_animator.GetBool("isInAir"))
                {
                    _animator.SetBool("isInAir", false);
                }
            }
        }

        void Jump()
        {
            _rb2D.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
        }

        void StartJumpRoutine()
        {
            if (_entity.IsAlive)
            {
                StartCoroutine(JumpRoutine());
            }
        }

        void StopJumpRoutine()
        {
            if (_animator.GetBool("isInAir"))
            {
                _animator.SetBool("isInAir", false);
            }
        }

        void StopXAxisMovement()
        {
            if (_xMovementActive)
            {
                _xMovementActive = false;
            }
            if (_animator.GetBool("isMoving"))
            {
                _animator.SetBool("isMoving", false);
            }
        }

        void StartXAxisMovement()
        {
            if (!_xMovementActive && _entity.IsAlive)
            {
                _xMovementActive = true;
                StartCoroutine(XAxisMovementRoutine());
            }
        }

        void XAxisMovement()
        {
            if (_targetingSystem.TargetTransform)
            {
                if (Mathf.Round(this.transform.position.x) != Mathf.Round(_targetingSystem.TargetTransform.position.x))
                {
                    if (this.transform.position.x < _targetingSystem.TargetTransform.position.x)
                    {
                        transform.Translate(new Vector3(0.025f * _movementSpeed, 0));
                        transform.localScale = new Vector3(_initScale.x * -1, _initScale.y, _initScale.z);
                    }
                    else
                    {
                        transform.Translate(new Vector3(0.025f * _movementSpeed * -1, 0));
                        transform.localScale = _initScale;
                    }
                    if (!_animator.GetBool("isMoving"))
                    {
                        _animator.SetBool("isMoving", true);
                    }
                }
                else
                {
                    if (_animator.GetBool("isMoving"))
                    {
                        _animator.SetBool("isMoving", false);
                    }
                }
            }
        }
        #endregion
    }
}