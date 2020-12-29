using System.Collections.Generic;
using UnityEngine;
using Core.StatusEffect;

namespace Triggerzone.Powerups
{
    public class Powerup : Triggerzone
    {
        [SerializeField] List<SingleStatusEffect> _StatusToActivate;
        protected override void Activate(Transform playerTransform)
        {
            foreach (SingleStatusEffect statusEffect in _StatusToActivate)
            {
                playerTransform.GetComponent<StatusEffectManager>().AddNewEffect(statusEffect);
            }
            Destroy(this.gameObject);
        }
    }
}

