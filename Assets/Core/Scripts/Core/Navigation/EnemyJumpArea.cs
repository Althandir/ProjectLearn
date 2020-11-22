using UnityEngine;
using Enemy.AI;

namespace Enemy.Navigation
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyJumpArea : MonoBehaviour
    {
        private void Start()
        {
            if (GetComponent<BoxCollider2D>().isTrigger == false)
            {
                Debug.LogWarning("BoxCollider Trigger isn't set correctly");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            EnemyAI enemyAI = collision.GetComponent<EnemyAI>();

            if (enemyAI)
            {
                enemyAI.CanJump = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            EnemyAI enemyAI = collision.GetComponent<EnemyAI>();

            if (enemyAI)
            {
                enemyAI.CanJump = false;
            }
        }
    }
}

