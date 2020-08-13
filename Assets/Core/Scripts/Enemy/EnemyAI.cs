using Core.Attack;
using Enemy;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 1.0f;
    //[SerializeField] float _jumpPower = 1f;

    [SerializeField] float _xMovementUpdateDelay = .01f;
    //[SerializeField] float _jumpDelay = 3f;

    [SerializeField] int _damageValue = 20;

    //Rigidbody2D _rigidbody2D;
    Vector3 _initScale;
    Animator _animator;
    Enemy.Attack.EnemyAttackArea _attackArea;
    EnemyEntity _entity;

    [SerializeField] Transform _targetTransform;

    bool _xMovementActive;
    //Coroutine _yjumpRoutine;

    #region Unity
    private void Awake()
    {
        //_rigidbody2D = GetComponent<Rigidbody2D>();
        _initScale = transform.localScale;
        _animator = GetComponent<Animator>();
        _entity = GetComponent<EnemyEntity>();
    }

    private void Start()
    {
        LinkToAttackArea();
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

    void PlayerInRangeDetected()
    {
        StartCoroutine(PlayerInRangeRoutine());
    }

    IEnumerator PlayerInRangeRoutine()
    {
        while (_attackArea.PlayerDetected && _entity.IsAlive)
        {
            _animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.1f);
        }
    }
    #region Animator Methods
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
            yield return new WaitForSeconds(_xMovementUpdateDelay);
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
            yield return new WaitForSeconds(_jumpDelay);
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
        if (Mathf.Round (this.transform.position.x) != Mathf.Round (_targetTransform.position.x))
        {
            if (this.transform.position.x < _targetTransform.position.x)
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
    #endregion
}
