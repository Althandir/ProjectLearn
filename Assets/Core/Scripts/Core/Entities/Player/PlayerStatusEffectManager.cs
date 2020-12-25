using Core.StatusEffect;
using UnityEngine;

namespace Player.StatusEffect
{
    /// <summary>
    /// Class used to enable different statuseffects on the player
    /// </summary>
    public class PlayerStatusEffectManager : StatusEffectManager
    {
        [SerializeField] PlayerEntity _playerEntity;
        [SerializeField] GameObject _statusEffectsObject;

        /// <summary>
        /// Adds a new Statuseffect on the effects object to influence the player
        /// </summary>
        /// <param name="effect"></param>
        public override void Activate(SingleStatusEffect effect)
        {
            // TODO!
        }

        #region Unity Messages
        private void Awake()
        {
            if (!_playerEntity)
            {
                Debug.LogError("Missing PlayerEntityReference in " + typeof(PlayerStatusEffectManager).Name);
            }
            if (!_statusEffectsObject)
            {
                Debug.LogError("Missing StatusEffectObject in " + typeof(PlayerStatusEffectManager).Name);
            }
        }

        #endregion
    }
}
