using System.Collections.Generic;
using UnityEngine;
using Player.StatusEffect;

namespace Triggerzone.Powerups
{
    public class Powerup : Triggerzone
    {
        [SerializeField] List<PlayerStatusEffect> _StatusToActivate;
        protected override void Activate(Transform playerTransform)
        {
            foreach (PlayerStatusEffect statusEffect in _StatusToActivate)
            {
                playerTransform.GetComponent<PlayerStatusEffectManager>().Activate(statusEffect);
            }
            Destroy(this.gameObject);
        }
    }
}

