using System;
using Global.Localization;
using IncrementalMode.Messaging;
using LoginRegister;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class FightingButton : MonoBehaviour
    {
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color enableColor;
        [SerializeField] private Color disableColor;

        [SerializeField] private MessageTextController messageTextController;
        

        private void Start()
        {
            buttonImage.color = LoginController.UserSignIn() ? enableColor : disableColor;
        }

        public void Click()
        {
            if (LoginController.UserSignIn())
            {
                SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
                return;
            }
            messageTextController.ShowMessage(LocalizationManager.GetWordByKey("login-required"), 5f);
        }
    }
}