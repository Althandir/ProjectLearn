using System.Collections.Generic;
using UnityEngine;
using Targetable;

namespace Core.Attack
{
    public class AttackAreaPush : AttackArea
    {
        [SerializeField] Transform _pushOrigin;
        [SerializeField] Transform _pushDirection;
        [SerializeField] float _pushPowerX = 150.0f;
        [SerializeField] float _pushPowerY = 250.0f;

        override public void OnAttack(int damageValue)
        {
            CalcPushDirection();
            // Temp list to prevent Errors of moditified collection during iteration in foreach
            List<Collider2D> temp_InRange = new List<Collider2D>(_inAttackRange);

            foreach (Collider2D item in temp_InRange)
            {
                item.GetComponent<Hitable>().OnHit(damageValue, CalcPushDirection());
            }
        } 

        private Vector2 CalcPushDirection()
        {
            return new Vector2(
                (_pushDirection.position.x - _pushOrigin.position.x)*_pushPowerX,
                (_pushDirection.position.y - _pushOrigin.position.y)*_pushPowerY);
        }
    }
}

