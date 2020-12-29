using UnityEngine;
using System.Collections;

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

        public void Initialize(SingleStatusEffect effect, Entity target)
        {
            _effect = effect;
            _target = target;

            StartCoroutine(StatusEffectRoutine());
        }

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
            Destroy(this);
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
                                    Debug.Log("Added " +_effect.Value + " Life!");
                                }
                                break;
                            case ModifierAction.Subtract:
                                {
                                    _target.DecreaseHitpoints((int)_effect.Value);
                                    Debug.Log("Removed Life!");
                                }
                                break;
                            case ModifierAction.Multiply:
                                {
                                    _target.MultiplyHitpoints(_effect.Value);
                                    Debug.Log("Multiplied Life!");
                                }
                                break;
                            case ModifierAction.Divide:
                                {
                                    _target.DivideHitpoints(_effect.Value);
                                    Debug.Log("Divided Life!");
                                }
                                break;
                            case ModifierAction.Set:
                                {
                                    _target.SetHitpoints((int)_effect.Value);
                                    Debug.Log("Set Life!");
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
                Debug.Log("Next Tick!");
                return true;
            }
            return false;
        }
    }
}