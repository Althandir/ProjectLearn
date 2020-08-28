using UnityEngine;

namespace Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class EntityAudioManager : MonoBehaviour
    {
        [SerializeField] AudioClip _movingAudio;
        [SerializeField] [Range(0.1f, 1)] float _movingAudioVolume;

        [SerializeField] AudioClip _attackAudio;
        [SerializeField] [Range(0.1f, 1)] float _attackAudioVolume;

        [SerializeField] AudioClip _jumpAudio;
        [SerializeField] [Range(0.1f, 1)] float _jumpAudioVolume;

        AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayMovingAudio()
        {
            _audioSource.PlayOneShot(_movingAudio, _movingAudioVolume);
        }

        public void PlayAttackAudio()
        {
            _audioSource.PlayOneShot(_attackAudio, _attackAudioVolume);
        }

        public void PlayJumpAudio()
        {
            _audioSource.PlayOneShot(_jumpAudio, _jumpAudioVolume);
        }
    }
}

