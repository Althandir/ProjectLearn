using UnityEngine;
namespace Targetable
{
    // Baseclass for HitableObjects
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Hitbox : MonoBehaviour
    {
        [SerializeField] protected GameObject _OnHitParticleSystem;
        [SerializeField] protected Transform _particleOrigin;

        virtual public void OnHit(int damageValue)
        {
            Instantiate(_OnHitParticleSystem, _particleOrigin.position, Quaternion.identity);
        }

        virtual public void OnHit(int damageValue, Vector2 pushDirection)
        {
            Instantiate(_OnHitParticleSystem, _particleOrigin.position, Quaternion.identity);
        }

    }
}