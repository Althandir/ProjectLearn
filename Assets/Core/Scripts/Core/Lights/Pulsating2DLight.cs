using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Lights
{
    [RequireComponent(typeof(Light2D))]
    public class Pulsating2DLight : MonoBehaviour
    {
        protected Light2D _light;
        [SerializeField]
        [Range(0.1f,10)] 
        protected float _changeSpeed = 1;

        [SerializeField]
        [Range(2f, 10)]
        protected float _maxIntensity = 1;
        [SerializeField]
        [Range(0f, 8)]
        protected float _minIntensity = 0.1f;

        [SerializeField]
        protected bool _randomizeStart = false;
        [SerializeField]
        protected int _startSeed = 0;
        protected float _valueBetweenBothIntensities;

        virtual protected void Awake()
        {
            _light = GetComponent<Light2D>();
            _valueBetweenBothIntensities = _maxIntensity - 1;

            if (_randomizeStart)
            {
                _startSeed = Random.Range(0, 1000);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_maxIntensity - 2 != _minIntensity + 2)
            {
                _minIntensity = _maxIntensity - 2;
            }
            if (_minIntensity+2 != _maxIntensity)
            {
                _maxIntensity = _minIntensity+2;
            }
        }
#endif

        private void FixedUpdate()
        {
            float newLightIntensityValue = Mathf.Sin((_startSeed + Time.time) * _changeSpeed) + _valueBetweenBothIntensities;
            if (newLightIntensityValue < _maxIntensity && newLightIntensityValue > _minIntensity)
            {
                _light.intensity = newLightIntensityValue;
            }
        }
    }
}
