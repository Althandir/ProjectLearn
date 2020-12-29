using Enemy;
using UnityEngine;

namespace Targetable
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitboxEnemy : Hitbox
    {
        EnemyEntity _entity;
        Rigidbody2D _rb2D;

        private void Awake()
        {
            _entity = GetComponent<EnemyEntity>();
            _rb2D = GetComponent<Rigidbody2D>();
        }

        override public void OnHit(int damageValue)
        {
            base.OnHit(damageValue);
            _entity.DecreaseHitpoints(damageValue);
        }

        public override void OnHit(int damageValue, Vector2 pushDirection)
        {
            base.OnHit(damageValue, pushDirection);
            _entity.DecreaseHitpoints(damageValue);
            _rb2D.AddForce(pushDirection, ForceMode2D.Force);
        }
    }
}