using UnityEngine;
using DG.Tweening;
using UniRx;

namespace SuperSpinner.UI
{
    public class Pointer : MonoBehaviour
    {
        public float duration = 0.05f;
        public float strength = 20f;

        [SerializeField] private SpinnerReel _spinnerReel;

        private RectTransform _pointer;
        private ParticleSystem _sparkEffect;

        private void Start()
        {
            _sparkEffect = GetComponentInChildren<ParticleSystem>();
            _pointer = GetComponent<RectTransform>();

            if (_spinnerReel == null)
            {
                Debug.LogError("SpinnerReel not assigned to Pointer!");
                return;
            }

            _spinnerReel.IsSpinning
                .DistinctUntilChanged() 
                .Subscribe(spinning =>
                {
                    if (spinning)
                        StartJitter();
                    else
                        StopJitter();
                })
                .AddTo(this);
        }

        /// <summary>
        /// Starts the jitter animation effect on the pointer and plays the spark particle effect.
        /// The pointer will oscillate back and forth to simulate vibration while the spinner is active.
        /// </summary>
        public void StartJitter()
        {
            if (_sparkEffect != null)
                _sparkEffect.Play();
                
            _pointer.DOLocalRotate(new Vector3(0, 0, strength), duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        /// <summary>
        /// Stops the jitter animation effect on the pointer and stops the spark particle effect.
        /// Resets the pointer rotation to its default position.
        /// </summary>
        public void StopJitter()
        {
            if (_sparkEffect != null)
                _sparkEffect.Stop();
                
            _pointer.DOKill();
            _pointer.localRotation = Quaternion.identity;
        }
    }
}