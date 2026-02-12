using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperSpinner.UI
{
    public static class UIBootstrap
    {
        private const string UI_LOADING_SCREEN_SCENE = "Loading";

        /// <summary>
        /// Loads the UI loading screen scene additively before any other scene is loaded, if it is not already present.
        /// </summary>
        /// <remarks>This method is automatically invoked before the first scene is loaded at runtime. If the UI
        /// loading screen scene is already loaded, this method does nothing. The scene is loaded additively, allowing it to
        /// overlay the current scene without replacing it.</remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void LoadUI()
        {
            // Check if the UI scene is already loaded to prevent duplicates
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == UI_LOADING_SCREEN_SCENE) return;
            }

            // Load the UI scene additively (stays on top of the current scene)
            SceneManager.LoadScene(UI_LOADING_SCREEN_SCENE, LoadSceneMode.Additive);
        }
    }
}