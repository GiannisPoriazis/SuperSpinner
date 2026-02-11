using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SuperSpinner.UI
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class SpinnerReel : MonoBehaviour
    {
        private RectTransform _wheelTransform;
        private float _itemHeight;
        private int _totalItems;
        private List<long> _values;
        private float _durationPerLoop = 0.5f;
        private const string PREFAB_PATH = "SpinnerValue";

        [HideInInspector] public BoolReactiveProperty IsSpinning = new BoolReactiveProperty(false);

        private void Start()
        {
            _wheelTransform = GetComponent<RectTransform>();
            _wheelTransform.pivot = new Vector2(0.5f, 1f);
            _values = GameManager.SpinnerValues.Value.spinnerValues.ToList();
            _totalItems = _values.Count;

            GameObject prefab = Resources.Load<GameObject>(PREFAB_PATH);
            RectTransform prefabRect = prefab.GetComponent<RectTransform>();

            float spacing = 0;
            spacing = GetComponent<VerticalLayoutGroup>().spacing;
            _itemHeight = prefabRect.rect.height + spacing;

            for (int i = 0; i < 3; i++)
            {
                foreach (var value in _values)
                {
                    GameObject reelValue = Instantiate(Resources.Load<GameObject>(PREFAB_PATH), transform);
                    reelValue.GetComponent<TextMeshProUGUI>().text = value.ToString();
                }
            }

            _wheelTransform.anchoredPosition = new Vector2(0, _totalItems * _itemHeight);
        }

        public async Task SpinToPrizeAsync(long winningValue)
        {
            IsSpinning.Value = true;

            _wheelTransform.DOKill();
            int fullSpins = Random.Range(4, 7);
            float loopHeight = _totalItems * _itemHeight;
            _wheelTransform.anchoredPosition = new Vector2(0, loopHeight);

            for (int i = 0; i < fullSpins; i++)
            {
                await _wheelTransform.DOAnchorPosY(loopHeight * 2, _durationPerLoop)
                    .SetEase(Ease.Linear)
                    .AsyncWaitForCompletion();

                _wheelTransform.anchoredPosition = new Vector2(0, loopHeight);
            }

            int winningIndex = _values.IndexOf(winningValue);
            float finalTargetY = loopHeight + (winningIndex * _itemHeight);

            float viewportHeight = ((RectTransform)transform.parent).rect.height;
            float centerOffset = (viewportHeight / 2) - (_itemHeight / 2);
            finalTargetY -= centerOffset;

            IsSpinning.Value = false;

            await _wheelTransform.DOAnchorPosY(finalTargetY, 1.5f)
                .SetEase(Ease.OutCubic)
                .AsyncWaitForCompletion();
        }
    }
}