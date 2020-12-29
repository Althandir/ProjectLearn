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

        public abstract int Hitpoints { get ;}

        public abstract void EntityDeathHandler();

        #region Hitpoint functions

        /// <summary>
        /// Add hitpoints of the entity by value. 
        /// </summary>
        /// <param name="value"></param>
        public virtual void AddHitpoints(int value)
        {
            if (_currentHitpoints + value > _maxHitpoints)
            {
                _currentHitpoints = _maxHitpoints;
            }
            else
            {
                _currentHitpoints += value;
            }
        }

        /// <summary>
        /// Decrease hitpoints of the entity by value. 
        /// </summary>
        /// <param name="value"></param>
        public virtual void DecreaseHitpoints(int value)
        {
            _currentHitpoints -= value;
            if (_currentHitpoints <= 0)
            {
                EntityDeathHandler();
            }
        }

        /// <summary>
        /// Multiply hitpoints of the entity by value. 
        /// </summary>
        /// <param name="value"></param>
        public virtual void MultiplyHitpoints(float value)
        {
            if (_currentHitpoints * value > _maxHitpoints)
            {
                _currentHitpoints = _maxHitpoints;
            }
            else
            {
                _currentHitpoints = (int)(_currentHitpoints * value);
            }

            if (_currentHitpoints <= 0)
            {
                EntityDeathHandler();
            }
        }

        /// <summary>
        /// Divides hitpoints of the entity by value. 
        /// </summary>
        /// <param name="value"></param>
        public virtual void DivideHitpoints(float value)
        {
            if (value != 0)
            {
                _currentHitpoints = (int)(_currentHitpoints / value);
                if (_currentHitpoints <= 0)
                {
                    EntityDeathHandler();
                }
            }
            else
            {
                Debug.LogError("Division through Zero not possible!");
            }
        }

        /// <summary>
        /// Sets hitpoints of the entity
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetHitpoints(int value)
        {
            if (value > _maxHitpoints)
            {
                _currentHitpoints = _maxHitpoints;
            }
            else if (value <= 0)
            {
                _currentHitpoints = value;
                EntityDeathHandler();
            }
            else
            {
                _currentHitpoints = value;
            }
        }

        #endregion




    }
}