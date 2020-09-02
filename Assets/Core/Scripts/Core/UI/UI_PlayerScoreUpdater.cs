using UnityEngine;
using TMPro;

namespace GameUI
{ 
    public class UI_PlayerScoreUpdater : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;

        private void Start()
        {
            Player.PlayerScore.Instance.OnScoreChanged.AddListener(ApplyNewScore);
            ApplyNewScore(0);
        }

        private void ApplyNewScore(long value)
        {
            _text.text = value.ToString();
        }
    }
}
