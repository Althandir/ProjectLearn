using Enemy;
using UnityEngine;

public class HitableEnemy : Hitable
{
    EnemyEntity _entity;

    private void Awake()
    {
        _entity = GetComponent<EnemyEntity>();
    }

    override public void OnHit(int damageValue)
    {
        _entity.Hitpoints =- damageValue;
    }

}
