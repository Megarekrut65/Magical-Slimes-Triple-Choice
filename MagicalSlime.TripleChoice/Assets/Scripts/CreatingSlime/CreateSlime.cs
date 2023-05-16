﻿using Global;
using Global.Localization;
using IncrementalMode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CreatingSlime
{
    public class CreateSlime: MonoBehaviour
    {
        [SerializeField] private InputField nameInput;
        [SerializeField] private Text errorMessage;

        public void Submit()
        {
            string slimeName = nameInput.text;
            if (slimeName.Length > 3)
            {
                DataSaver.SaveLevel(DataSaver.LoadLevel()/2);
                DataSaver.RemoveSlimeData();
                DataSaver.SaveSlimeName(slimeName);
                DataSaver.SaveShop("life", 1);

                SceneManager.LoadScene("IncrementalMode", LoadSceneMode.Single);
                return;
            }

            errorMessage.text = LocalizationManager.GetWordByKey("name-length");
        }
    }
}