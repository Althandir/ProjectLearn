using Core.AttackSystem;
using UnityEngine;
using UnityEngine.Events;
using Targetable;
using System.Collections.Generic;

namespace Enemy.Attack
{
    public class EnemyAttackArea : AttackArea
    {
        UnityEvent _playerDetectedEvent = new UnityEvent();
        bool _playerDetected;

        public UnityEvent PlayerDetectedEvent { get => _playerDetectedEvent; }
        public bool PlayerDetected { get => _playerDetected; }

        override public void OnAttack(int damageValue)
        {
            // Temp list to prevent Errors of moditified collection during iteration in foreach
            List<Collider2D> temp_InRange = new List<Collider2D>(_inAttackRange);

            foreach (Collider2D item in temp_InRange)
            {
                // prevents enemies of killing each other
                if (item.GetComponent<HitboxEnemy>())
                {
                    continue;
                }
                item.GetComponent<Hitbox>().OnHit(damageValue);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.GetComponent<HitboxPlayer>())
            {
                _playerDetected = true;
                _playerDetectedEvent.Invoke();
            }
        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);

            if (collision.GetComponent<HitboxPlayer>())
            {
                _playerDetected = false;
            }
        }
    }
}
