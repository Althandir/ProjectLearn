using Core.City;
using System.Collections;
using UnityEngine;

namespace Lights
{
    public class Pulsating2DLightGate : Pulsating2DLight
    {
        UnityEngine.Color _initColor;
        bool _colorChangeActive;
        [SerializeField] float _onEnemyEnteredColorChangeDuration = 2.0f;

        float counter = 0.0f;

        protected override void Awake()
        {
            base.Awake();
            _initColor = _light.color;
        }

        private void Start()
        {
            CityGate.StaticReference.EnemyEnteredGateEvent.AddListener(OnEnemyEnteredGate);
        }

        private void OnEnemyEnteredGate()
        {
            ResetCounter();

            if (!_colorChangeActive)
            {
                _colorChangeActive = true;
                StartCoroutine(ColorChangeRoutine());
            }
        }

        IEnumerator ColorChangeRoutine()
        {
            _light.color = new Color(1, 0, 0, _initColor.a / 2);
            
            while (_colorChangeActive)
            {
                yield return new WaitForFixedUpdate();
                if (counter > _onEnemyEnteredColorChangeDuration)
                {
                    _colorChangeActive = false;
                }
                else
                {
                    counter += Time.fixedDeltaTime;
                }        
            }

            ResetCounter();
            _light.color = _initColor;
        }

        private void ResetCounter()
        {
            counter = 0.0f;
        }
    }
}

