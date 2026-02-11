using UnityEngine;
using DG.Tweening;
using UniRx;

namespace SuperSpinner.UI
{
    public class Pointer : MonoBehaviour
    {
        public float duration = 0.05f;
        public float strength = 20f;

        private RectTransform _pointer;
        private ParticleSystem _sparkEffect;

        private void Start()
        {
            _sparkEffect = GetComponentInChildren<ParticleSystem>();
            _pointer = GetComponent<RectTransform>();
            SpinnerReel wheel = FindFirstObjectByType<SpinnerReel>();

            wheel.IsSpinning
                .Subscribe(spinning =>
                {
                    if (spinning)
                        StartJitter();
                    else
                        StopJitter();
                })
                .AddTo(this);
        }

        public void StartJitter()
        {
            _sparkEffect.Play();
            _pointer.DOLocalRotate(new Vector3(0, 0, strength), duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void StopJitter()
        {
            _sparkEffect.Stop();
            _pointer.DOKill();
            _pointer.localRotation = Quaternion.identity;
        }
    }
}