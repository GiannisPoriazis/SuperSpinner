using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperSpinner.UI
{
    public class UIManager: MonoBehaviour
    {
        private LoadingScreen _loadingScreen;
        private const string UI_LOADING_SCREEN_SCENE = "Loading Screen";
        private const string UI_SPINNER_SCREEN_SCENE = "Spinner Screen";

        private async void Start()
        {
            _loadingScreen = FindFirstObjectByType<LoadingScreen>();

            Task timeoutTask = Task.Delay(10000);
            Task completedTask = await Task.WhenAny(GameManager.InitTask, timeoutTask);

            if (completedTask == GameManager.InitTask && await GameManager.InitTask)
            {
                await OnDataLoadedAsync();
            }
        }

        private async Task OnDataLoadedAsync()
        {
            await _loadingScreen.FadeOutUI();
            await SceneManager.LoadSceneAsync(UI_SPINNER_SCREEN_SCENE, LoadSceneMode.Additive);
            await SceneManager.UnloadSceneAsync(UI_LOADING_SCREEN_SCENE);
            Destroy(_loadingScreen);
        }
    }
}
