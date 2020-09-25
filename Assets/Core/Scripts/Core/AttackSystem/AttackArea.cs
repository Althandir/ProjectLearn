using System.Collections.Generic;
using UnityEngine;
using Targetable;

namespace Core.AttackSystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AttackArea : MonoBehaviour
    {
        protected List<Collider2D> _inAttackRange = new List<Collider2D>();

        virtual public void OnAttack(int damageValue)
        {
            // Temp list to prevent Errors of moditified collection during iteration in foreach
            List<Collider2D> temp_InRange = new List<Collider2D>(_inAttackRange);

            foreach (Collider2D item in temp_InRange)
            {
                item.GetComponent<Hitbox>().OnHit(damageValue);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Hitbox>())
            {
                _inAttackRange.Add(collision);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (_inAttackRange.Contains(collision))
            {
                _inAttackRange.Remove(collision);
            }
        }
    }
}

