using UnityEngine;
using UnityEngine.Events;

namespace Triggerzone.Traps
{
    public class BaseTrap : Triggerzone
    {
        [SerializeField] int damageValue;

        protected override void Activate(Transform enemyTransform)
        {
            enemyTransform.GetComponent<Targetable.HitboxEnemy>().OnHit(damageValue);
        }
    }
}


