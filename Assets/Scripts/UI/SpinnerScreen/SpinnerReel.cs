using TMPro;
using UnityEngine;

namespace SuperSpinner.UI
{
    public class SpinnerScreen: MonoBehaviour
    {
        private void Start()
        {
            foreach(var value in GameManager.SpinnerValues.Value.spinnerValues)
            {
                GameObject reelValue = Instantiate(Resources.Load<GameObject>("SpinnerValue"), transform);
                reelValue.GetComponent<TextMeshProUGUI>().text = value.ToString();
            }
        }
    }
}
