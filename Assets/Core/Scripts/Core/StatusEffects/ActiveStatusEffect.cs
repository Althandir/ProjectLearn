using System.Collections;
using UnityEngine;

namespace Core.StatusEffect
{
    /// <summary>
    /// Class to represent every single StatusEffect
    /// 
    /// </summary>
    /// 
    public class ActiveStatusEffect : MonoBehaviour
    {
        [Header("Debug Values")]
        [SerializeField] Entity _target;
        [SerializeField] SingleStatusEffect _effect;
        [SerializeField] float _timer;
        [SerializeField] ParticleSystem _particleSystem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effect">Values of the StatusEffect</param>
        /// <param name="target">Entity to be affected by the StatusEffect</param>
        /// <param name="particlesParentTransform">Transform which will hold the particleSystem created by this StatusEffect</param>
        public void Initialize(SingleStatusEffect effect, Entity target, Transform particlesParentTransform)
        {
            _effect = effect;
            _target = target;

            InitParticleSystem(particlesParentTransform, target);
            StartCoroutine(StatusEffectRoutine());
        }

        private void InitParticleSystem(Transform particlesParent, Entity target)
        {
            if (!particlesParent)
            {
                Debug.LogError("Missing particleParentObject for " + target.name);
            }

            if (!_effect.ParticleSystem)
            {
                Debug.LogWarning("No ParticleSystem found for " + _effect.name);
            }
            else
            {
                _particleSystem = Instantiate(_effect.ParticleSystem, particlesParent).GetComponent<ParticleSystem>();
            }
        }

        /// <summary>
        /// Disables the corresponding ParticleSystem by setting the StopAction to Destroy 
        /// and disabling the looping of the ParticleSystem.
        /// </summary>
        private void DisableParticleSystem()
        {
            if (_particleSystem)
            {
                ParticleSystem.MainModule main = _particleSystem.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
                main.loop = false;
                // _particleSystem.main.loop = false; << don't work
            }
        }

        /// <summary>
        /// MainRoutine of the StatusEffect. Started after Initilize. <see cref="Initialize(SingleStatusEffect, Entity, Transform)"/>
        /// </summary>
        /// <returns></returns>
        IEnumerator StatusEffectRoutine()
        {
            if (_effect.TotalDurationInSeconds == 0)
            {
                ApplyEffect();
            }
            else
            {
                while (_timer < _effect.TotalDurationInSeconds)
                {
                    yield return IncreaseTimer();
                    if (CheckForNextTick())
                    {
                        ApplyEffect();
                    }
                }
            }
            RemoveStatusEffect();
        }

        private void ApplyEffect()
        {
            switch (_effect.modifiedValue)
            {
                case ModifiedValue.Health:
                    {
                        switch (_effect.Action)
                        {
                            case ModifierAction.Add:
                                {
                                    _target.AddHitpoints((int) _effect.Value);
                                }
                                break;
                            case ModifierAction.Subtract:
                                {
                                    _target.DecreaseHitpoints((int)_effect.Value);
                                }
                                break;
                            case ModifierAction.Multiply:
                                {
                                    _target.MultiplyHitpoints(_effect.Value);
                                }
                                break;
                            case ModifierAction.Divide:
                                {
                                    _target.DivideHitpoints(_effect.Value);
                                }
                                break;
                            case ModifierAction.Set:
                                {
                                    _target.SetHitpoints((int)_effect.Value);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        WaitForSeconds IncreaseTimer()
        {
            _timer = (float) System.Math.Round(_timer + .1f,1) ;
            return new WaitForSeconds(.1f);
        }
          
        bool CheckForNextTick()
        {
            if (Mathf.Approximately(_timer % _effect.TickDuration, 0.0f))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method is called when Entity dies or the StatusEffect is expired
        /// </summary>
        public void RemoveStatusEffect()
        {
            StopAllCoroutines();
            DisableParticleSystem();
            Destroy(this);
        }

    }
}