using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// TODO: Finish BuffSystem
/// </summary>

namespace Player.StatusEffect
{
    public class PlayerStatusEffectManager : MonoBehaviour
    {
        [SerializeField] List<ActiveStatusEffect> _activeEffects;

        private void Start()
        {
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
            }
        }


        public void Activate(PlayerStatusEffect newStatusEffect)
        {
            ActiveStatusEffect tmpEffect = SearchAlreadyAppliedStatus(newStatusEffect);
            if (tmpEffect == null)
            {
                _activeEffects.Add(new ActiveStatusEffect(newStatusEffect));
            }
            else
            {
                tmpEffect.ResetTimer();
            }
        }


        /// <summary>
        /// Checks if the new StatusEffect is already applied to the player and if so returns it.
        /// </summary>
        /// <param name="newStatusEffect"></param>
        /// <returns></returns>
        /// 
        ActiveStatusEffect SearchAlreadyAppliedStatus(PlayerStatusEffect newStatusEffect)
        {
            foreach (ActiveStatusEffect activeEffect in _activeEffects)
            {
                if (activeEffect.Effect == newStatusEffect)
                {
                    return activeEffect;
                }
            }
            return null;
        }
    }
    
    [System.Serializable]
    class ActiveStatusEffect
    {
        [SerializeField] PlayerStatusEffect effect;
        [SerializeField] float totalTimer = 0.0f;
        [SerializeField] bool isExpired = false;

        public PlayerStatusEffect Effect { get => effect;}

        public ActiveStatusEffect(PlayerStatusEffect effect)
        {
            this.effect = effect;
        }

        bool NextTick()
        {
            if (totalTimer % effect.TickDuration >= effect.TickDuration)
            {
                return true;
            }
            return false;
        }

        public bool Expired()
        {
            if (totalTimer > effect.TotalDurationInSeconds)
            {
                return true;
            }
            return false;
        }

        public void ResetTimer()
        {
            totalTimer = 0.0f;
        }

        void ApplyEffect()
        {
            Debug.Log("Apply Effect to Player!");
        }
    }
    


}
