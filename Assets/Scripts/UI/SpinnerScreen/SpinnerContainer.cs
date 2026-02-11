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

        private RectTransform _wheelContainer;

        private void Start()
        {
            _wheelContainer = GetComponent<RectTransform>();
            SpinnerReel wheel = FindFirstObjectByType<SpinnerReel>();

            wheel.IsSpinning
                .Subscribe(isSpinning => HandleZoom(isSpinning))
                .AddTo(this);

        }
        private void HandleZoom(bool isSpinning)
        {
            _wheelContainer.DOKill();

            float targetScale = isSpinning ? zoomScale : 1f;

            _wheelContainer.DOScale(targetScale, zoomDuration)
                .SetEase(isSpinning ? Ease.OutBack : Ease.OutCubic);
        }
    }
}
