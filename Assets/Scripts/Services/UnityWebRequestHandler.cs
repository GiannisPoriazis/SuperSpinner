using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SuperSpinner.Services
{
    public class UnityWebRequestHandler : IWebRequestHandler
    {
        /// <summary>
        /// Sends a GET request to the specified URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="timeout">Request timeout in seconds.</param>
        /// <returns>The response text if successful, null otherwise.</returns>
        public async Task<string> GetAsync(string url, int timeout)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = timeout;

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    return request.downloadHandler.text;
                }
                else
                {
                    Debug.LogError($"GET Request Error: {request.error}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Sends a POST request to the specified URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="timeout">Request timeout in seconds.</param>
        /// <returns>The response text if successful, null otherwise.</returns>
        public async Task<string> PostAsync(string url, int timeout)
        {
            using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, string.Empty))
            {
                request.timeout = timeout;

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    return request.downloadHandler.text;
                }
                else
                {
                    Debug.LogError($"POST Request Error: {request.error}");
                    return null;
                }
            }
        }
    }
}
