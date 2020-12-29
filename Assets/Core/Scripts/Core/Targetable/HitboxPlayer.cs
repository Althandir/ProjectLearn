using Player;

namespace Targetable
{
    public class HitboxPlayer : Hitbox
    {
        PlayerEntity _entity;
        private void Awake()
        {
            _entity = transform.parent.GetComponent<PlayerEntity>();
        }

        override public void OnHit(int damageValue)
        {
            base.OnHit(damageValue);
            _entity.DecreaseHitpoints(damageValue);
        }
    }
}