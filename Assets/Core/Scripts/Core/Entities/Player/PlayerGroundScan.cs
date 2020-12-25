using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.GroundScan
{
    public class PlayerGroundScan : MonoBehaviour
    {
        bool _isGrounded;

        public bool IsGrounded { get => _isGrounded; }

        private void OnTriggerEnter2D(Collider2D detectedCollider)
        {
            if (detectedCollider.GetComponent<Identification.TilemapFloor>())
            {
                _isGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D detectedCollider)
        {
            if (detectedCollider.GetComponent<Identification.TilemapFloor>())
            {
                _isGrounded = false;
            }
        }
    }
}
