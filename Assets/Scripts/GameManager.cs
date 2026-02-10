using SuperSpinner.Models;
using SuperSpinner.Services;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SuperSpinner
{
    public static class GameManager
    {
        private static TaskCompletionSource<bool> _initSource = new TaskCompletionSource<bool>();
        public static Task<bool> InitTask => _initSource.Task;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void LoadData()
        {
            try
            {
                var data = await SpinnerService.GetSpinnerValues();

                if (data != null && data.spinnerValues.Length > 0)
                {
                    Debug.Log($"Successfully loaded {data.spinnerValues.Length} values.");
                    _initSource.SetResult(true);
                    // Do something with the data here

                }
                else                 {
                    Debug.LogError("No spinner values found in the response.");
                    _initSource.SetResult(false);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error while loading spinner values: {e.Message}");
                _initSource.SetResult(false);
            }
        }
    }
}
