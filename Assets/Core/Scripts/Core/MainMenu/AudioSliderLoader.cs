using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{ 
    [RequireComponent(typeof(Slider))]
    public class AudioSliderLoader : MonoBehaviour
    {
        [SerializeField] AudioSetting _source;

        void Start()
        {
            Slider slider = GetComponent<Slider>();
            Core.GameSettings gameSettingsRef = Core.GameSettings.Instance;
            switch (_source)
            {
                case AudioSetting.Master:
                    slider.SetValueWithoutNotify(gameSettingsRef.GetMasterVolume());
                    break;
                case AudioSetting.Music:
                    slider.SetValueWithoutNotify(gameSettingsRef.GetMusicVolume());
                    break;
                case AudioSetting.Effects:
                    slider.SetValueWithoutNotify(gameSettingsRef.GetEffectsVolume());
                    break;
                default:
                    Debug.LogWarning("AudioSliderLoader has no sourcevalue");
                    break;
            }
        }
    }

    enum AudioSetting
    {
        Master, Music, Effects
    }
}