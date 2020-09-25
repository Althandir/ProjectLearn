using UnityEngine;
using UnityEngine.UI;

namespace GameUI.Sliders
{
    [RequireComponent(typeof(Slider))]
    public class UI_SliderLifeUpdater : MonoBehaviour
    {
        protected Slider _slider;

        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        virtual protected void Start()
        {
            Debug.LogError("Base of UI_SliderLifeUpdater called.");
        }

        protected void OnLifeChanged(int value)
        {
            _slider.value = value;
        }
    }
}

