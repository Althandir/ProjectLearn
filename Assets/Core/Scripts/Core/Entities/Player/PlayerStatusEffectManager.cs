using System;
using UnityEngine;

namespace Core.StatusEffect
{
    /// <summary>
    /// Class used to enable different statuseffects on the player
    /// </summary>
    public class PlayerStatusEffectManager : StatusEffectManager
    {
        [SerializeField] Player.PlayerEntity _playerEntity;

        public override void AddNewEffect(SingleStatusEffect effect)
        {
            ActiveStatusEffect newStatusEffect = (ActiveStatusEffect) _statusEffectsObject.AddComponent(typeof(ActiveStatusEffect));
            newStatusEffect.Initialize(effect, _playerEntity, _statusEffectsParticleTransform);
            Debug.Log("Added new Status on Player!");
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

            _playerEntity.PlayerDeadEvent.AddListener(OnPlayerDead);
        }

        private void OnPlayerDead()
        {
            ActiveStatusEffect[] activeStatusEffects = _statusEffectsObject.GetComponents<ActiveStatusEffect>();
            foreach (ActiveStatusEffect effect in activeStatusEffects)
            {
                effect.RemoveStatusEffect();
            }
        }
        #endregion
    }
}
