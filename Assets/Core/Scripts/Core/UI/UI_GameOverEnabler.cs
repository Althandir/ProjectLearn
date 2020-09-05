using UnityEngine;

namespace GameUI.GameOver
{
    public class UI_GameOverEnabler : MonoBehaviour
    {
        [SerializeField] GameObject _UI_GameOver;

        void Start()
        {
            Core.City.CityValues.Instance.CityDestroyedEvent.AddListener(HandleCityDeath);
        }

        private void HandleCityDeath()
        {
            _UI_GameOver.SetActive(true);
        }
    }
}