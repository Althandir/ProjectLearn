namespace GameUI
{
    public class UI_PlayerScoreUpdater : UI_TextUpdater
    {
        override protected void Start()
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