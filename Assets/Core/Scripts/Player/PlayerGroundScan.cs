using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.GroundScan
{
    public class PlayerGroundScan : MonoBehaviour
    {
        bool _isGrounded;
        List<Collider2D> _detectedColliders = new List<Collider2D>();

        public bool IsGrounded { get => _isGrounded; }

        private void OnTriggerEnter2D(Collider2D detectedCollider)
        {
            if (detectedCollider.GetComponent<TilemapCollider2D>())
            {
                _detectedColliders.Add(detectedCollider);
            }
            GroundCheck();
        }

        private void OnTriggerExit2D(Collider2D detectedCollider)
        {
            if (detectedCollider.GetComponent<TilemapCollider2D>())
            {
                if (_detectedColliders.Contains(detectedCollider))
                {
                    _detectedColliders.Remove(detectedCollider);
                }
                GroundCheck();
            }
        }

        void GroundCheck()
        {
            if (_detectedColliders.Count == 0)
            {
                _isGrounded = false;
            }
            else
            {
                _isGrounded = true;
            }
        }
    }
}
