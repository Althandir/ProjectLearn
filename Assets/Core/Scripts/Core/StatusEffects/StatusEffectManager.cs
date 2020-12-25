using UnityEngine;

namespace Core.StatusEffect
{
    /// <summary>
    /// Baseclass for EnemyStatusEffectManager & PlayerStatusEffectManager
    /// Manager used to add new statuseffects on an entity. 
    /// </summary>
    public abstract class StatusEffectManager : MonoBehaviour
    {
        public abstract void Activate(SingleStatusEffect effect);
    }
}