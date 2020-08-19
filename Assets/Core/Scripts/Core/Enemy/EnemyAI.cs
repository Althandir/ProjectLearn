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
        [SerializeField] int _damageValue = 20;


        Vector3 _initScale;
        Animator _animator;

        EnemyEntity _entity;
        Attack.EnemyAttackArea _attackArea;
        Targeting.EnemyTargeting _targetingSystem;

        bool _xMovementActive;

        UnityEvent _StartTrackingPlayerEvent = new UnityEvent();

        #region Unity
        private void Awake()
        {
            _initScale = transform.localScale;
            _animator = GetComponent<Animator>();
            _entity = GetComponent<EnemyEntity>();

            LinkToTargetingSystem();
            LinkToAttackArea();
        }

        private void Start()
        {

        }

        private void LinkToTargetingSystem()
        {
            _targetingSystem = GetComponentInChildren<Targeting.EnemyTargeting>();
        }

        private void OnEnable()
        {
            StartXAxisMovement();
            //_yjumpRoutine = StartCoroutine(JumpRoutine());
        }


        private void OnDisable()
        {
            StopXAxisMovement();
            //StopCoroutine(_yjumpRoutine);
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
        }

        void OnAttackComplete()
        {
            _attackArea.OnAttack(_damageValue);
        }

        void OnAttackEnd()
        {
            StartXAxisMovement();
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

        /*
        private IEnumerator JumpRoutine()
        {
            while(true)
            {
                if (this.transform.position.y < _targetTransform.position.y || this.transform.position.y > _targetTransform.position.y)
                {
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpPower);
                }
                yield return new WaitForFixedUpdate();
            }

        }*/

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