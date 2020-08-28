using UnityEngine;
namespace Targetable
{
    // Baseclass for HitableObjects
    [RequireComponent(typeof(BoxCollider2D))]
    public class Hitable : MonoBehaviour
    {
        // ??? Why did I created this Event?
        HitEvent _wasHit = new HitEvent();

        virtual public void OnHit(int damageValue)
        {
            Debug.LogWarning("Object was hit. Invoking _wasHit Event with " + damageValue + " damage.");
            _wasHit.Invoke(damageValue);
        }

        virtual public void OnHit(int damageValue, Vector2 pushDirection)
        {
            Debug.LogWarning("Object was hit. Invoking _wasHit Event with " + damageValue + " damage. Pushing Object with " + pushDirection);
            _wasHit.Invoke(damageValue);
        }

    }
}