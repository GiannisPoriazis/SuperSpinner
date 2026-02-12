using DG.Tweening;
using SuperSpinner.Services;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SuperSpinner.UI
{
    [RequireComponent(typeof(Button), typeof(CanvasGroup))]
    public class Spin: MonoBehaviour
    {
        [SerializeField] private SpinnerReel _reel;
        [SerializeField] private Prize _prize;

        private CanvasGroup _maskCG;
        private CanvasGroup _spinCG;
        private RectTransform _spinTransform;

        [Header("Drop-In Animation Settings")]
        [SerializeField] private float _dropDistance = 50f;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Start()
        {
            _maskCG = transform.parent.GetComponent<CanvasGroup>();
            _spinCG = GetComponent<CanvasGroup>();
            _spinTransform = GetComponent<RectTransform>();
            GetComponent<Button>().onClick.AddListener(OnSpinClicked);
        }

        /// <summary>
        /// Handles the spin button click event. Initiates the spin sequence, displays the prize animation,
        /// and re-enables the UI controls after completion.
        /// </summary>
        private async void OnSpinClicked()
        {
            DisableCG(_maskCG);
            DisableCG(_spinCG);

            var result = await SpinnerService.Instance.GetSpinnerResult();
            Debug.Log(result.spinnerValue.ToString());
            await _reel.SpinToPrizeAsync(result.spinnerValue);

            _maskCG.alpha = 1;

            await _prize.PlayPrizeAnimation(result.spinnerValue);
            await ShowTapToSpin();

            _maskCG.interactable = true;
            _spinCG.interactable = true;
        }

        /// <summary>
        /// Disables a canvas group by setting its alpha to 0 and making it non-interactable.
        /// </summary>
        /// <param name="canvasGroup">The canvas group to disable.</param>
        private void DisableCG(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }

        /// <summary>
        /// Animates the "Tap to Spin" button by dropping it in from above with a fade-in effect.
        /// </summary>
        /// <returns>A task that completes when the drop-in animation finishes.</returns>
        private async Task ShowTapToSpin()
        {
            _spinCG.DOKill();
            transform.DOKill();

            _spinTransform.anchoredPosition = new Vector2(0, _dropDistance);

            Sequence dropInSeq = DOTween.Sequence();

            await dropInSeq.Join(_spinCG.DOFade(1f, _fadeDuration))
                     .Join(_spinTransform.DOAnchorPosY(0, _fadeDuration).SetEase(Ease.OutCubic))
                     .AsyncWaitForCompletion();
        }
    }
}
