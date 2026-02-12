using System.Threading.Tasks;

namespace SuperSpinner.Services
{
    public interface IWebRequestHandler
    {
        /// <summary>
        /// Sends a GET request to the specified URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="timeout">Request timeout in seconds.</param>
        /// <returns>The response text if successful, null otherwise.</returns>
        Task<string> GetAsync(string url, int timeout);

        /// <summary>
        /// Sends a POST request to the specified URL.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="timeout">Request timeout in seconds.</param>
        /// <returns>The response text if successful, null otherwise.</returns>
        Task<string> PostAsync(string url, int timeout);
    }
}
