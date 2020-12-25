using UnityEngine;

/// <summary>
/// Baseclass for the Player, every Enemy, every Object in the Game
/// </summary>
/// 
namespace Core
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected int _maxHitpoints;
        [SerializeField] protected int _currentHitpoints;

        protected Animator _animator;

        public abstract int Hitpoints { get; set; }
    }
}