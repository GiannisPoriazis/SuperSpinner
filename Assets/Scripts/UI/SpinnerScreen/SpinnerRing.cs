using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SuperSpinner.UI
{
    public class SpinnerRing: MonoBehaviour
    {
        [Header("Flicker Settings")]
        public float minAlpha = 0.3f;
        public float maxAlpha = 1.0f;
        public float flickerSpeed = 0.1f;

        private Image _ringImage;

        private void Start()
        {
            _ringImage = GetComponent<Image>();
            StartFlicker();
        }

        /// <summary>
        /// Starts a continuous flicker animation on the ring image, fading between minimum and maximum alpha values.
        /// The animation loops indefinitely to create a pulsing visual effect.
        /// </summary>
        private void StartFlicker()
        {
            DOTween.Sequence()
                .Append(_ringImage.DOFade(minAlpha, flickerSpeed).SetEase(Ease.InOutSine))
                .Append(_ringImage.DOFade(maxAlpha, flickerSpeed).SetEase(Ease.InOutSine))
                .SetLoops(-1);
        }
    }
}
