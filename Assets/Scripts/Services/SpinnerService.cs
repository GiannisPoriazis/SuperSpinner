using SuperSpinner.Config;
using SuperSpinner.Models;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SuperSpinner.Services
{
    public static class SpinnerService
    {
        private static string _cachedUrl;

        private static async Task<string> GetApiUrlAsync()
        {
            var config = Resources.Load<Configuration>("Configuration");
            return config != null ? config.apiUrl : null;
        }

        public static async Task<SpinnerData> GetSpinnerValues()
        {
            _cachedUrl = await GetApiUrlAsync();

            if (string.IsNullOrEmpty(_cachedUrl))
            {
                Debug.LogError("API URL cound not be found.");
                return null;
            }

            using (UnityWebRequest request = UnityWebRequest.Get($"{_cachedUrl}spinner/values"))
            {
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
    }
}