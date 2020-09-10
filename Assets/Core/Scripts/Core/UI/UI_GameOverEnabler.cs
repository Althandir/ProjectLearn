using System.Collections;
using UnityEngine;

namespace GameUI.GameOver
{
    public class UI_GameOverEnabler : MonoBehaviour
    {
        [SerializeField] GameObject _UI_GameOver;
        [SerializeField] float _delayTime = 5.0f;
        void Start()
        {
            Core.City.CityValues.Instance.CityDestroyedEvent.AddListener(HandleCityDeath);
        }

        private void HandleCityDeath()
        {
            Core.City.CityValues.Instance.CityDestroyedEvent.RemoveListener(HandleCityDeath);
            StartCoroutine(GameOverUIEnableRoutine());
        }

        private IEnumerator GameOverUIEnableRoutine()
        {
            yield return new WaitForSecondsRealtime(_delayTime);
            _UI_GameOver.SetActive(true);
        }
    }
}