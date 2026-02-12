using System.Linq;
using UnityEngine;

namespace SuperSpinner.Audio
{
    public class AudioManager : Singleton<AudioManager>, IAudioManager
    {
        private AudioSource _audioSource;
        private AudioClip _clickSound;
        private AudioClip _prizeSound;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = FindFirstObjectByType<AudioSource>();
            _clickSound = Resources.Load<AudioClip>("Audio/spinning_sound");
            _prizeSound = Resources.Load<AudioClip>("Audio/prize_sound");
        }

        /// <summary>
        /// Plays a click sound with a randomized pitch variation.
        /// </summary>
        public void PlayClickSound()
        {
            _audioSource.pitch = Random.Range(0.95f, 1.05f);
            _audioSource.PlayOneShot(_clickSound);
        }

        /// <summary>
        /// Plays a prize sound with pitch scaled based on the prize value.
        /// Higher prizes produce higher pitch sounds.
        /// </summary>
        /// <param name="prize">The prize value used to determine the pitch of the sound.</param>
        public void PlayPrizeSound(long prize)
        {
            var values = GameManager.SpinnerValues.Value.spinnerValues.ToList();

            float minPrize = values.Min();
            float maxPrize = values.Max();
            float minPitch = 0.8f;
            float maxPitch = 1.5f;

            float t = Mathf.InverseLerp(minPrize, maxPrize, prize);
            float targetPitch = Mathf.Lerp(minPitch, maxPitch, t);

            _audioSource.pitch = targetPitch;
            _audioSource.PlayOneShot(_prizeSound);
        }
    }
}
