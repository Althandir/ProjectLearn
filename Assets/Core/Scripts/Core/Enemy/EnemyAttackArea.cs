using Core.Attack;
using UnityEngine;
using UnityEngine.Events;
using Targetable;

namespace Enemy.Attack
{
    public class EnemyAttackArea : AttackArea
    {
        UnityEvent _playerDetectedEvent = new UnityEvent();
        bool _playerDetected;

        public UnityEvent PlayerDetectedEvent { get => _playerDetectedEvent; }
        public bool PlayerDetected { get => _playerDetected; }

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
