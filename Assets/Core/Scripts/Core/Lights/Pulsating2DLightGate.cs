using Core.City;
using System;
using System.Collections;
using UnityEngine;

namespace Lights
{
    public class Pulsating2DLightGate : Pulsating2DLight
    {
        Color _initColor;
        bool _colorChangeActive;

        [SerializeField] float _onEnemyEnteredColorChangeDuration = 2.0f;
        [SerializeField] Color _targetColor = Color.red;

        float counter = 0.0f;

        protected override void Awake()
        {
            base.Awake();
            _initColor = _light.color;
        }

        private void Start()
        {
            CityGate.Instance.EnemyEnteredGateEvent.AddListener(OnEnemyEnteredGate);
            CityValues.Instance.CityDestroyedEvent.AddListener(OnCityDeath);
        }

        private void OnCityDeath()
        {
            CityGate.Instance.EnemyEnteredGateEvent.RemoveListener(OnEnemyEnteredGate);
            StopAllCoroutines();
            _colorChangeActive = true;
            _light.color = _targetColor;
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
            _light.color = _targetColor;
            
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

