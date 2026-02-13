using SuperSpinner.Config;
using SuperSpinner.Models;
using System.Threading.Tasks;
using UnityEngine;

namespace SuperSpinner.Services
{
    public class SpinnerService : Singleton<SpinnerService>, ISpinnerService
    {
        private string _cachedUrl;
        private const int REQUEST_TIMEOUT = 10;
        private IWebRequestHandler _webRequestHandler;

        protected override void Awake()
        {
            base.Awake();
            _webRequestHandler = new UnityWebRequestHandler();
        }

        /// <summary>
        /// Sets a custom web request handler for testing purposes.
        /// </summary>
        /// <param name="handler">The web request handler to use.</param>
        public void SetWebRequestHandler(IWebRequestHandler handler)
        {
            _webRequestHandler = handler;
        }

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
                ErrorHandler.Instance.TriggerError("Network related error. Please try again later.");
                return null;
            }

            try
            {
                string json = await _webRequestHandler.GetAsync($"{apiUrl}spinner/values", REQUEST_TIMEOUT);

                if (!string.IsNullOrEmpty(json))
                {
                    return JsonUtility.FromJson<SpinnerData>(json);
                }

                ErrorHandler.Instance.TriggerError("Network related error. Please try again later.");
                return null;
            }
            catch(System.Exception ex) 
            { 
                Debug.LogError($"Error fetching spinner values: {ex.Message}");
                ErrorHandler.Instance.TriggerError("Network related error. Please try again later.");
                return null; 
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
                ErrorHandler.Instance.TriggerError("Network related error. Please try again later.");
                return null;
            }

            try
            {
                string json = await _webRequestHandler.PostAsync($"{apiUrl}spinner/spin", REQUEST_TIMEOUT);

                if (!string.IsNullOrEmpty(json))
                {
                    return JsonUtility.FromJson<SpinnerResult>(json);
                }

                ErrorHandler.Instance.TriggerError("Network related error. Please try again later.");
                return null;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error fetching spinner result: {ex.Message}"); 
                ErrorHandler.Instance.TriggerError("Network related error. Please try again later.");
                return null;
            }
        }
    }
}