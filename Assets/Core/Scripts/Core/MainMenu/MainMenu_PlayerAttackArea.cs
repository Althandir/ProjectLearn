using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class MainMenu_PlayerAttackArea : MonoBehaviour
{
    [SerializeField] float _delayUntilNextAttack = 2.0f;
    float _delayTimer = 0;
    bool _enemyDetected;

    [SerializeField] Animator _animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy.EnemyEntity>())
        {
            _enemyDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy.EnemyEntity>())
        {
            _enemyDetected = false;
        }
    }

    private void FixedUpdate()
    {
        _delayTimer += Time.fixedDeltaTime;
        if (_delayTimer > _delayUntilNextAttack && _enemyDetected)
        {
            _delayTimer = 0.0f;
            _animator.SetTrigger("Attacking");
        }
    }
}
