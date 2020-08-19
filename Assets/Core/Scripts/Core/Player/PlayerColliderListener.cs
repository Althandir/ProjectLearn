using UnityEngine;

namespace Player.ColliderListener
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerColliderListener : MonoBehaviour
    {
        private void Awake()
        {
            transform.parent.GetComponent<PlayerEntity>().OnPlayerDead.AddListener(DisableCollider);
        }

        void DisableCollider()
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

