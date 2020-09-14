using System.Collections;
using UnityEngine;
using Targetable;

namespace Enemy.AI.Targeting
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyTargeting : MonoBehaviour
    {
        [SerializeField] bool _playerDetected;

        [SerializeField] float _MaxTrackingTime = 5.0f;
        [SerializeField] Transform _targetTransform;
        [SerializeField] float counter = 0.0f;

        public Transform TargetTransform { get => _targetTransform; }

        #region Unity Messages

        private void OnEnable()
        {
            TargetGate();
            StartCoroutine(TrackingRoutine());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<HitboxPlayer>())
            {
                _playerDetected = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<HitboxPlayer>())
            {
                _playerDetected = false;
            }
        }
        #endregion

        IEnumerator TrackingRoutine()
        {

            while (gameObject.activeSelf)
            {
                if (_playerDetected)
                {
                    counter = 0.0f;
                    TargetPlayer();
                }
                else
                {
                    counter += Time.fixedDeltaTime;
                    if (counter > _MaxTrackingTime)
                    {
                        TargetGate();
                    }
                }
                yield return new WaitForFixedUpdate();
            }
        }

        private void TargetGate()
        {
            if (_targetTransform != Core.City.CityGate.Instance.transform)
            {
                _targetTransform = Core.City.CityGate.Instance.transform;
            }
        }

        private void TargetPlayer()
        {
            if (_targetTransform != Player.PlayerEntity.Instance.transform)
            {
                _targetTransform = Player.PlayerEntity.Instance.transform;
            }
        }

    }
}
