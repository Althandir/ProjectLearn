using UnityEngine;
using TMPro;

namespace GameUI.GameOver
{
    [RequireComponent(typeof(TMP_Text))]
    public class UI_GameOverStats : MonoBehaviour
    {
        TMP_Text _text;

        [SerializeField] string _savedPeopleText;
        [SerializeField] string _WaveCounterText;
        [SerializeField] string _playtimeText;

        #region Unity Messages
        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = _savedPeopleText + ": XXXX \n" + _WaveCounterText +": XX \n"  +  _playtimeText + ": XXXXs";
        }

        private void OnEnable()
        {
            _text = GetComponent<TMP_Text>();

            _text.text = string.Format(
                _savedPeopleText + ": {0} \n " +
                _WaveCounterText + ": {1} \n " +
                _playtimeText + ": {2}",
                Player.PlayerScore.Instance.Score.ToString(), Core.Spawning.WaveManager.Instance.WaveCounter.ToString(), Time.realtimeSinceStartup.ToString("N0"));
        }
        #endregion
    }
}