using DG.Tweening;
using SuperSpinner.Audio;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;

namespace SuperSpinner.UI
{
    [RequireComponent(typeof(TextMeshProUGUI), typeof(CanvasGroup))]
    public class Prize: MonoBehaviour
    {
        private RectTransform _prizeTransform;
        private TextMeshProUGUI _prizeText;
        private CanvasGroup _prizeCG;
        private Canvas _prizeCanvas;

        private void Start()
        {
            _prizeTransform = GetComponent<RectTransform>();
            _prizeText = GetComponent<TextMeshProUGUI>();
            _prizeCG = GetComponent<CanvasGroup>();
            _prizeCanvas = GetComponent<Canvas>();

            TogleCG(false);
        }

        private void TogleCG(bool active)
        {
            _prizeCG.interactable = active;
            _prizeCG.alpha = active ? 1 : 0;
            _prizeCG.blocksRaycasts = active;
        }

        /// <summary>
        /// Plays an animated sequence to display the prize value with scaling and fade effects.
        /// The animation includes a scale-up, brief pause, scale-down, and fade-out sequence.
        /// </summary>
        /// <param name="value">The prize value to display during the animation.</param>
        /// <returns>A task that completes when the animation sequence finishes.</returns>
        public async Task PlayPrizeAnimation(long value)
        {
            TogleCG(true);

            _prizeCanvas.overrideSorting = true;
            _prizeCanvas.sortingOrder = 100;
            _prizeText.alpha = 1f;
            _prizeText.text = value.ToString();
            _prizeTransform.DOKill();
            _prizeText.DOKill();
            _prizeTransform.localScale = Vector3.one;

            AudioManager.Instance?.PlayPrizeSound(value);

            Sequence prizeSeq = DOTween.Sequence();

            await prizeSeq
                  .Append(_prizeTransform.DOScale(1.5f, 0.3f).SetEase(Ease.OutBack))
                  .AppendInterval(1.5f)
                  .Append(_prizeTransform.DOScale(1f, 1f).SetEase(Ease.InCubic))
                  .Append(_prizeText.DOFade(0f, 0.5f))
                  .AsyncWaitForCompletion();

            _prizeCanvas.overrideSorting = false;
            _prizeCanvas.sortingOrder = 0;

            TogleCG(false);
        }
    }
}
