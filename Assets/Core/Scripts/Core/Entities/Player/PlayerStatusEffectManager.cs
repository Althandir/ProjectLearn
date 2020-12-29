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
            newStatusEffect.Initialize(effect, _playerEntity);
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
        }
        #endregion
    }
}
