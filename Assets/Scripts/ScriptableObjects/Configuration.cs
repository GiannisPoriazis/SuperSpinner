using UnityEngine;

namespace SuperSpinner.Config
{
    [CreateAssetMenu(fileName = "Configuration", menuName = "ScriptableObjects/Configuration")]
    public class Configuration : ScriptableObject
    {
        public string apiUrl = "https://platform00.abzorbagames.com/eplatform/";
    }
}