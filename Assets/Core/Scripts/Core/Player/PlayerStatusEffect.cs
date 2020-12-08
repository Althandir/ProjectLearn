using UnityEngine;

namespace Player.StatusEffect
{
    [CreateAssetMenu]
    public class PlayerStatusEffect : ScriptableObject
    {
        [SerializeField] TargetValue _targetValue;
        [SerializeField] float _value;
        [SerializeField] ModifierAction _action;
        [SerializeField] float _totalDurationInSeconds;
        [SerializeField] float _tickDuration;

        public float Value { get => _value; }
        public TargetValue TargetValue { get => _targetValue; }
        public ModifierAction Action { get => _action;  }
        public float TotalDurationInSeconds { get => _totalDurationInSeconds;}
        public float TickDuration { get => _tickDuration; }
    }

    public enum TargetValue
    {
        Health
    }

    public enum ModifierAction
    {
        Add, Subtract, Multiply, Divide, Set
    }
}