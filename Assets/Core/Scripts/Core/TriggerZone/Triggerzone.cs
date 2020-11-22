using UnityEngine;

namespace Triggerzone
{
    /// <summary>
    /// BaseClass for Powerups & Traps. 
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Triggerzone : MonoBehaviour
    {
        [SerializeField] Target _target;

        private void Awake()
        {
            if (!GetComponent<Collider2D>().isTrigger)
            {
                Debug.LogError("Collider in Triggerzone is not set as Trigger in: " + transform.gameObject.name);
                this.enabled = false;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            CheckForTarget(other);
        }

        void CheckForTarget(Collider2D other)
        {
            switch (_target)
            {
                case Target.Enemy:
                    {
                        if (other.GetComponent<Targetable.HitboxEnemy>())
                        {
                            Activate(other.transform);
                        }
                        break;
                    }
                case Target.Player:
                    {
                        if (other.GetComponent<Targetable.HitboxPlayer>())
                        {
                            Activate(other.transform.parent);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Method to be implemented by every deriving class to activate.
        /// Base is sending a log into the console.
        /// </summary>
        protected virtual void Activate(Transform otherTransform)
        {
            Debug.Log("Triggerzone activated.");
        }

    }

    /// <summary>
    /// Enum to differentiate between possible Targets
    /// </summary>
    enum Target
    {
        Enemy, Player
    }
}
