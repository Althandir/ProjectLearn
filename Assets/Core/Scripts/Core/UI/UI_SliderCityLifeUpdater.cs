using Core.City;
namespace GameUI.Sliders
{
    public class UI_SliderCityLifeUpdater : UI_SliderLifeUpdater
    {
        override protected void Start()
        {
            CityValues.StaticReference.CityLifeChangedEvent.AddListener(OnLifeChanged);

            _slider.maxValue = CityValues.StaticReference.MaxCityLife;
        }
    }
}