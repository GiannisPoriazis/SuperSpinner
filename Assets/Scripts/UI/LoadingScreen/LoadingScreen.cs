using System.Threading.Tasks;
using UnityEngine;

namespace SuperSpinner.UI
{
    public class LoadingScreen: MonoBehaviour
    {
        private CanvasGroup fadeGroup;

        private void Start()
        {
            fadeGroup = GetComponent<CanvasGroup>();
            fadeGroup.alpha = 1;
        }

        public async Task FadeOutUI()
        {
            while (fadeGroup.alpha > 0)
            {
                fadeGroup.alpha -= Time.deltaTime * 2;
                await Task.Yield();
            }
        }
    }
}
