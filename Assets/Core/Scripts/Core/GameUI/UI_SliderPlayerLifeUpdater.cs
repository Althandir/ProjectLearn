using Player;

namespace GameUI.Sliders
{
    public class UI_SliderPlayerLifeUpdater : UI_SliderLifeUpdater
    {
        protected override void Start()
        {
            PlayerEntity.Instance.OnPlayerLifeChanged.AddListener(OnLifeChanged);

            _slider.maxValue = PlayerEntity.Instance.MaxHitpoints;
            // Should be done somehow better :/
            _slider.value = PlayerEntity.Instance.MaxHitpoints;
        }
    }
}