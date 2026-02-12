using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperSpinner.UI
{
    public class UIManager: MonoBehaviour
    {
        private LoadingScreen _loadingScreen;
        private const string UI_LOADING_SCREEN_SCENE = "Loading";
        private const string UI_SPINNER_SCREEN_SCENE = "Spinner";

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

        /// <summary>
        /// Handles the scene transition after data is successfully loaded.
        ///  Loads the spinner screen, fades out and unloads the loading screen.
        /// </summary>
        /// <returns>A task that completes when all scene transitions are finished.</returns>
        private async Task OnDataLoadedAsync()
        {
            var loadScene = SceneManager.LoadSceneAsync(UI_SPINNER_SCREEN_SCENE, LoadSceneMode.Additive);

            while (!loadScene.isDone)
            {
                await Task.Yield();
            }

            await _loadingScreen.FadeOutUI();
            await SceneManager.UnloadSceneAsync(UI_LOADING_SCREEN_SCENE);
            if (_loadingScreen != null) Destroy(_loadingScreen.gameObject);
        }
    }
}
