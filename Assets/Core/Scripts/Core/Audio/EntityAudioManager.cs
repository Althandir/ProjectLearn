using UnityEngine;

namespace Core.Audio
{
    public class EntityAudioManager : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;

        [SerializeField] AudioClip _movingAudio;
        [SerializeField] [Range(0.1f, 1)] float _movingAudioVolume;

        [SerializeField] AudioClip _attackingAudio;
        [SerializeField] [Range(0.1f, 1)] float _attackAudioVolume;

        [SerializeField] AudioClip _jumpAudio;
        [SerializeField] [Range(0.1f, 1)] float _jumpAudioVolume;

        [SerializeField] AudioClip _damagedAudio;
        [SerializeField] [Range(0.1f, 1)] float _damagedAudioVolume;

        public void PlayMovingAudio()
        {
            _audioSource.PlayOneShot(_movingAudio, _movingAudioVolume);
        }

        public void PlayAttackingAudio()
        {
            _audioSource.PlayOneShot(_attackingAudio, _attackAudioVolume);
        }

        public void PlayJumpAudio()
        {
            _audioSource.PlayOneShot(_jumpAudio, _jumpAudioVolume);
        }

        public void PlayDamagedAudio()
        {
            _audioSource.PlayOneShot(_damagedAudio, _damagedAudioVolume);
        }
    }
}

