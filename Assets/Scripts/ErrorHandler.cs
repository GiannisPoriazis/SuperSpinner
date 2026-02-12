using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperSpinner
{
    public class ErrorHandler: Singleton<ErrorHandler>
    {
        private const string UI_ERROR_SCREEN_SCENE = "Error";

        [SerializeField] private TextMeshProUGUI _errorMessageText;

        public async void TriggerError(string message)
        {
            var loadScene = SceneManager.LoadSceneAsync(UI_ERROR_SCREEN_SCENE, LoadSceneMode.Additive);

            while (!loadScene.isDone)
            {
                await Task.Yield();
            }

            _errorMessageText = GameObject.FindGameObjectWithTag("ErrorMessage").GetComponent<TextMeshProUGUI>();

            if (_errorMessageText)
            {
                _errorMessageText.text = message;
            }
        }
    }
}
