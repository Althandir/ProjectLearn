using UnityEngine;
using UnityEngine.Events;

// Baseclass for HitableObjects
[RequireComponent(typeof(BoxCollider2D))]
public class Hitable : MonoBehaviour
{
    HitEvent _wasHit = new HitEvent();

    virtual public void OnHit(int damageValue)
    {
        Debug.LogWarning("Object was hit. Invoking _wasHit Event with " + damageValue + " damage.");
        _wasHit.Invoke(damageValue);
    }

}
