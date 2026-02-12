using DG.Tweening;
using UnityEngine;
using UniRx;

namespace SuperSpinner.UI
{
    public class SpinnerContainer: MonoBehaviour
    {
        [Header("Zoom Settings")]
        public float zoomScale = 1.1f; 
        public float zoomDuration = 0.5f; 
        
        private const float FOCUS_DELAY = 1.5f;

        private RectTransform _wheelContainer;

        private void Start()
        {
            _wheelContainer = GetComponent<RectTransform>();
            SpinnerReel wheel = FindFirstObjectByType<SpinnerReel>();

            wheel.IsSpinning
                .DistinctUntilChanged() 
                .Subscribe(isSpinning => HandleZoom(isSpinning))
                .AddTo(this);
        }

        /// <summary>
        /// Handles the zoom animation of the spinner container based on the spinning state.
        /// Zooms in when spinning starts and zooms out with a delay when spinning stops.
        /// </summary>
        /// <param name="isSpinning">The state of the spinner.</param>
        private void HandleZoom(bool isSpinning)
        {
            _wheelContainer.DOKill();

            float targetScale = isSpinning ? zoomScale : 1f;
            float currentDelay = isSpinning ? 0f : FOCUS_DELAY;

            _wheelContainer.DOScale(targetScale, zoomDuration)
                .SetEase(isSpinning ? Ease.OutBack : Ease.OutCubic)
                .SetDelay(currentDelay);
        }
    }
}
