using UnityEngine;

namespace SuperSpinner.UI
{
    public class SpinnerCanvas: MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }
}