using UnityEngine;
using System.Collections.Generic;

namespace Core.StatusEffect
{
    /// <summary>
    /// Baseclass for EnemyStatusEffectManager & PlayerStatusEffectManager
    /// Manager used to add new statuseffects on an entity. 
    /// </summary>
    public abstract class StatusEffectManager : MonoBehaviour
    {
        [SerializeField] protected GameObject _statusEffectsObject;
        /// <summary>
        /// Adds a new ActiveStatuseffect on the StatusEffectObjectHolder which will influence the entity
        /// </summary>
        /// <param name="effect">Values of the SingleStatusEffect.</param>
        /// <seealso cref="SingleStatusEffect"/>
        public abstract void AddNewEffect(SingleStatusEffect effect);
    }
}