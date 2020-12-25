using UnityEngine;

namespace Player.ColliderListener
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerColliderListener : MonoBehaviour
    {
        private void Awake()
        {
            transform.parent.GetComponent<PlayerEntity>().PlayerDeadEvent.AddListener(DisableCollider);
            transform.parent.GetComponent<PlayerEntity>().PlayerRespawnEvent.AddListener(EnableCollider);
        }

        void EnableCollider()
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

        void DisableCollider()
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

