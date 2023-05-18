using System;
using UnityEngine;

namespace Global
{
    /// <summary>
    /// Store data to local memory of device.
    /// </summary>
    public static class LocalStorage {

        public static void Remove(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        public static T GetValue<T>(string key, T def)
        {
            if (!PlayerPrefs.HasKey(key)) SetValue(key, def);
            return def switch
            {
                int => (T)(object)PlayerPrefs.GetInt(key),
                float => (T)(object)PlayerPrefs.GetFloat(key),
                _ => (T)(object)PlayerPrefs.GetString(key)
            };
        }
        public static void SetValue<T>(string key, T value)
        {
            switch (value)
            {
                case int:
                    PlayerPrefs.SetInt(key,  Convert.ToInt32(value));
                    break;
                case float:
                    PlayerPrefs.SetFloat(key,  Convert.ToSingle(value));
                    break;
                default:
                    PlayerPrefs.SetString(key, $"{value}");
                    break;
            }
        }
    }
}