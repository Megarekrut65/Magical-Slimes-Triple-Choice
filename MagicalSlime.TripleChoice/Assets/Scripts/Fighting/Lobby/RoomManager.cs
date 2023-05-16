﻿using Global.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Fighting.Lobby
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private Text errorText;
        [SerializeField] private GameObject loader;
        
        protected void Click()
        {
            errorText.text = "";
            loader.SetActive(true);
        }
        protected void Error(string key)
        {
            errorText.text = LocalizationManager.GetWordByKey(key);
            loader.SetActive(false);
        }
    }
}