using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class Pulsating2DLight : MonoBehaviour
{
    float _basevalue;
    Light2D _light;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _basevalue = _light.intensity;
    }

    private void FixedUpdate()
    {
        _light.intensity = _basevalue + Mathf.Sin(Time.time*3) / 2;
    }
}
