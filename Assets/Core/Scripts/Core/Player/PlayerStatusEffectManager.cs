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

        public void Activate(PlayerStatusEffect newStatusEffect)
        {
            if (!StatusAlreadyApplied(newStatusEffect))
            {
                _activeEffects.Add(new ActiveStatusEffect(newStatusEffect));
            }
        }

        
        /// <summary>
        /// Checks if the new StatusEffect is already applied to the player and if so resets it's timer.
        /// </summary>
        /// <param name="newStatusEffect"></param>
        /// <returns></returns>
        bool StatusAlreadyApplied(PlayerStatusEffect newStatusEffect)
        {
            foreach (ActiveStatusEffect activeEffect in _activeEffects)
            {
                if (activeEffect.Effect == newStatusEffect)
                {
                    activeEffect.ResetTimer();
                    return true;
                }
            }
            return false;
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

        IEnumerator Timer()
        {
            while (!Expired())
            {
                yield return new WaitForFixedUpdate();
                totalTimer += Time.fixedDeltaTime;
                if (NextTick())
                {
                    ApplyEffect();
                }
            }
            isExpired = true;
        }

        bool NextTick()
        {
            if (totalTimer % effect.TickDuration >= effect.TickDuration)
            {
                return true;
            }
            return false;
        }

        bool Expired()
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
