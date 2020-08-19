using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Lights
{
    [RequireComponent(typeof(Light2D))]
    public class Pulsating2DLight : MonoBehaviour
    {
        protected float _basevalue;
        protected Light2D _light;

        virtual protected void Awake()
        {
            _light = GetComponent<Light2D>();
            _basevalue = _light.intensity;
        }

        private void FixedUpdate()
        {
            _light.intensity = _basevalue + Mathf.Sin(Time.time * 3) / 2;
        }
    }
}
