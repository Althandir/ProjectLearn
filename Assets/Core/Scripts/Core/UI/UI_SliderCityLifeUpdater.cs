using Core.City;
namespace GameUI.Sliders
{
    public class UI_SliderCityLifeUpdater : UI_SliderLifeUpdater
    {
        override protected void Start()
        {
            CityValues.Instance.CityLifeChangedEvent.AddListener(OnLifeChanged);

            _slider.maxValue = CityValues.Instance.MaxCityLife;
            _slider.value = CityValues.Instance.MaxCityLife;
        }
    }
}