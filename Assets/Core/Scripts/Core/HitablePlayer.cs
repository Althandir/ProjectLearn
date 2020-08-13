using Player;

public class HitablePlayer : Hitable
{
    PlayerEntity _entity;

    private void Awake()
    {
        _entity = transform.parent.GetComponent<PlayerEntity>();
    }

    override public void OnHit(int damageValue)
    {
        _entity.Hitpoints = -damageValue;
    }
}
