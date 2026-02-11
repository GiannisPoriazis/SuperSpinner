using System.Threading.Tasks;
using UnityEngine;

namespace SuperSpinner.UI
{
    public class LoadingScreen: MonoBehaviour
    {
        private CanvasGroup _fadeGroup;

        private void Start()
        {
            _fadeGroup = GetComponent<CanvasGroup>();
            _fadeGroup.alpha = 1;
        }

        public async Task FadeOutUI()
        {
            while (_fadeGroup.alpha > 0)
            {
                _fadeGroup.alpha -= Time.deltaTime * 2;
                await Task.Yield();
            }
        }
    }
}
