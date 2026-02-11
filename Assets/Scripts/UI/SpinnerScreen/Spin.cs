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
        private SpinnerReel _reel;
        private Prize _prize;
        private CanvasGroup _maskCG;
        private CanvasGroup _spinCG;
        private RectTransform _spinTransform;

        [SerializeField] private float _dropDistance = 50f;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Start()
        {
            _reel = FindFirstObjectByType<SpinnerReel>();
            _prize = FindAnyObjectByType<Prize>();
            _maskCG = transform.parent.GetComponent<CanvasGroup>();
            _spinCG = GetComponent<CanvasGroup>();
            _spinTransform = GetComponent<RectTransform>();
            GetComponent<Button>().onClick.AddListener(OnSpinClicked);
        }

        private async void OnSpinClicked()
        {
            DisableCG(_maskCG);
            DisableCG(_spinCG);

            var result = await SpinnerService.GetSpinnerResult();
            Debug.Log(result.spinnerValue.ToString());
            await _reel.SpinToPrizeAsync(result.spinnerValue);

            _maskCG.alpha = 1;

            await _prize.PlayPrizeAnimation(result.spinnerValue);
            await ShowTapToSpin();

            _maskCG.interactable = true;
            _spinCG.interactable = true;
        }

        private void DisableCG(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }

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
