using System;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    /// <summary>
    /// Loads local storage boolean value to GUI toggle.
    /// </summary>
    public class LocalStorageToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private string key;

        private void Start()
        {
            string value = LocalStorage.GetValue(key, "false");
            toggle.isOn = bool.Parse(value);
            toggle.onValueChanged.AddListener(Changed);
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(Changed);
        }

        private void Changed(bool value)
        {
            LocalStorage.SetValue(key, value.ToString());
        }
    }
}