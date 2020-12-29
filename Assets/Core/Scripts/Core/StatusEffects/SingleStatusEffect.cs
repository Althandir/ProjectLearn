using UnityEngine;

namespace Core.StatusEffect
{
    [CreateAssetMenu]
    public class SingleStatusEffect : ScriptableObject
    {
        [SerializeField] ModifiedValue _modifiedValue;
        [SerializeField] float _value;
        [SerializeField] ModifierAction _action;
        [SerializeField] float _totalDurationInSeconds;
        [SerializeField] float _tickDuration;

        public float Value { get => _value; }
        public ModifiedValue modifiedValue { get => _modifiedValue; }
        public ModifierAction Action { get => _action;  }
        public float TotalDurationInSeconds { get => _totalDurationInSeconds;}
        public float TickDuration { get => _tickDuration; }
    }

    public enum ModifiedValue
    {
        Health
    }

    public enum ModifierAction
    {
        Add, Subtract, Multiply, Divide, Set
    }
}