using SuperSpinner.Config;
using SuperSpinner.Models;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SuperSpinner.Services
{
    public class SpinnerService : Singleton<SpinnerService>, ISpinnerService
    {
        private string _cachedUrl;
        private const int REQUEST_TIMEOUT = 10;

        private async Task<string> GetApiUrlAsync()
        {
            if (!string.IsNullOrEmpty(_cachedUrl))
                return _cachedUrl;

            var config = Resources.Load<Configuration>("Configuration");
            _cachedUrl = config != null ? config.apiUrl : null;
            return _cachedUrl;
        }

        /// <summary>
        /// Retrieves the available spinner values from the server.
        /// </summary>
        /// <returns>A task containing the spinner data with all available prize values, or null if the request fails.</returns>
        public async Task<SpinnerData> GetSpinnerValues()
        {
            string apiUrl = await GetApiUrlAsync();

            if (string.IsNullOrEmpty(apiUrl))
            {
                Debug.LogError("API URL could not be found.");
                return null;
            }

            using (UnityWebRequest request = UnityWebRequest.Get($"{apiUrl}spinner/values"))
            {
                request.timeout = REQUEST_TIMEOUT;

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;
                    return JsonUtility.FromJson<SpinnerData>(json);
                }
                else
                {
                    Debug.LogError($"Network Error: {request.error}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Requests a spin result from the server.
        /// </summary>
        /// <returns>A task containing the spin result with the awarded prize value, or null if the request fails.</returns>
        public async Task<SpinnerResult> GetSpinnerResult()
        {
            string apiUrl = await GetApiUrlAsync();

            if (string.IsNullOrEmpty(apiUrl))
            {
                Debug.LogError("API URL could not be found.");
                return null;
            }

            using (UnityWebRequest request = UnityWebRequest.PostWwwForm($"{apiUrl}spinner/spin", string.Empty))
            {
                request.timeout = REQUEST_TIMEOUT;

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;
                    return JsonUtility.FromJson<SpinnerResult>(json);
                }
                else
                {
                    Debug.LogError($"Network Error: {request.error}");
                    return null;
                }
            }
        }
    }
}