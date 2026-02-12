using SuperSpinner.Models;
using System.Threading.Tasks;

namespace SuperSpinner.Services
{
    public interface ISpinnerService
    {
        /// <summary>
        /// Retrieves the available spinner values from the server.
        /// </summary>
        /// <returns>A task containing the spinner data with all available prize values, or null if the request fails.</returns>
        Task<SpinnerData> GetSpinnerValues();

        /// <summary>
        /// Requests a spin result from the server.
        /// </summary>
        /// <returns>A task containing the spin result with the awarded prize value, or null if the request fails.</returns>
        Task<SpinnerResult> GetSpinnerResult();
    }
}
