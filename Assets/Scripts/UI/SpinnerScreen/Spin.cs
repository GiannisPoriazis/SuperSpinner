using SuperSpinner.Services;
using UnityEngine;
using UnityEngine.UI;

namespace SuperSpinner.UI
{
    public class Spin: MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnSpinClicked);
        }

        private async void OnSpinClicked()
        {
            Debug.Log("Spin button clicked!");
            var result = await SpinnerService.GetSpinnerResult();
            Debug.Log(result.spinnerValue.ToString());
        }
    }
}
