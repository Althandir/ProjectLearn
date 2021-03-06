using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Lights
{
    [RequireComponent(typeof(Light2D))]
    public class Pulsating2DLight : MonoBehaviour
    {
        protected Light2D _light;
        [SerializeField]
        [Range(0.1f,5)] 
        protected float _changeSpeed = 1;

        [SerializeField]
        [Range(0.1f, 10)]
        protected float _maxIntensity = 1;
        [SerializeField]
        [Range(0.001f, 1)]
        protected float _minIntensity = 0.1f;

        [SerializeField]
        protected bool _randomizeStart = false;
        [SerializeField]
        protected float _startSeed = 0;
        protected float _differenceBetweenBothIntensities;

        virtual protected void Awake()
        {
            _light = GetComponent<Light2D>();
            _differenceBetweenBothIntensities = _maxIntensity - _minIntensity;

            if (_randomizeStart)
            {
                _startSeed = Random.Range(0f, 1000f);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_minIntensity > _maxIntensity)
            {
                _maxIntensity = _minIntensity;
            }
            _differenceBetweenBothIntensities = _maxIntensity - _minIntensity;
        }
#endif

        private void FixedUpdate()
        {
            float newLightIntensityValue = (Mathf.Abs(Mathf.Sin((_startSeed + Time.time) * _changeSpeed) * _differenceBetweenBothIntensities)) + _minIntensity;
            if (newLightIntensityValue < _maxIntensity && newLightIntensityValue > _minIntensity)
            {
                _light.intensity = newLightIntensityValue;
            }
        }
    }
}
