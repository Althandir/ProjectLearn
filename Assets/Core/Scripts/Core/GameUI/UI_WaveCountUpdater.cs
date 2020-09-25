namespace GameUI
{
    public class UI_WaveCountUpdater : UI_TextUpdater
    {
        override protected void Start()
        {
            Core.Spawning.WaveManager.Instance.NextWaveEvent.AddListener(ApplyNextWave);
            ApplyNextWave(1);
        }

        private void ApplyNextWave(int value)
        {
            _text.text = value.ToString();
        }
    }
}
