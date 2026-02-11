using DG.Tweening;
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

        private void Start()
        {
            _prizeTransform = GetComponent<RectTransform>();
            _prizeText = GetComponent<TextMeshProUGUI>();
            _prizeCG = GetComponent<CanvasGroup>();

            TogleCG(false);
        }

        private void TogleCG(bool active)
        {
            _prizeCG.interactable = active;
            _prizeCG.alpha = active ? 1 : 0;
            _prizeCG.blocksRaycasts = active;
        }

        public async Task PlayPrizeAnimation(long value)
        {
            TogleCG(true);

            _prizeText.text = value.ToString();
            _prizeTransform.DOKill();
            _prizeTransform.localScale = Vector3.one;

            Sequence winSeq = DOTween.Sequence();

            await winSeq
                  .Append(_prizeTransform.DOScale(1.5f, 0.3f).SetEase(Ease.OutBack)) 
                  .AppendInterval(1.0f) 
                  .Append(_prizeTransform.DOScale(1.0f, 0.5f).SetEase(Ease.InCubic))
                  .Append(_prizeText.DOFade(0f, 0.4f))
                  .AsyncWaitForCompletion();

            TogleCG(false);
        }
    }
}
