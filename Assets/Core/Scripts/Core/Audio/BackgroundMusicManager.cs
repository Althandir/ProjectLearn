using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusicManager : MonoBehaviour
    {
        AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            GameSettings.Instance.MusicVolumeChangedEvent.AddListener(OnVolumeChange);
    }

        private void OnVolumeChange()
        {
            _source.volume = GameSettings.Instance.MusicVolume * GameSettings.Instance.MasterVolume;
        }
    }
}

