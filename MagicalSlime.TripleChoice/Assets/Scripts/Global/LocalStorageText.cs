using System;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    /// <summary>
    /// Loads value from local storage to GUI text.
    /// </summary>
    public class LocalStorageText : MonoBehaviour
    {
        [SerializeField] private Text text;

        [SerializeField] private string key;

        private void Start()
        {
            text.text = LocalStorage.GetValue(key, "");
        }
    }
}