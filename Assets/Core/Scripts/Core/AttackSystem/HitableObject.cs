using UnityEngine;

public class HitableObject : Hitable
{

    public override void OnHit(int damageValue)
    {
        Debug.LogWarning("Object has been hit.");
    }
}
