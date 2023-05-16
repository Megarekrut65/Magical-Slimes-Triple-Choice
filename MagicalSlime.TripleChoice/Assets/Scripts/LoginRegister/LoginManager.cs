﻿using System.Collections.Generic;
using Database;
using Firebase.Auth;
using Global;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LoginRegister
{
    public class LoginManager : MonoBehaviour
    {
        [SerializeField] private ScreenLoader loader;
        
        [SerializeField] private Text errorText;
        
        [SerializeField] protected InputField emailField;
        [SerializeField] protected InputField passwordField;

        [SerializeField] private ConflictManager conflictManager;

        public void Login()
        {
            loader.Show();
            Error("");
            string email = emailField.text;
            string password = passwordField.text;
            
            LoginController.Login(email, password, LoginAnswer);
        }

        private void LoginAnswer(bool success, string message)
        {
            if (success)
            {
                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                if (auth.CurrentUser == null)
                {
                    Answer(false, "some-error-login");
                    return;
                }
                
                UserController.AuthorizeUser(auth.CurrentUser.UserId, Answer, Conflict);
                return;
            }
            Answer(false, message);
        }

        private void Conflict(Dictionary<string, object> data)
        {
            Debug.Log("Conflict!");
            DatabaseSaver saver = new DatabaseSaver();
            conflictManager.Conflict(data, saver.GetAllData());
        }
        protected void Answer(bool success, string message)
        {
            loader.Hide();
            if (success)
            {
                SceneManager.LoadScene("Account", LoadSceneMode.Single);
                return;
            }
            Error(message);
        }
        protected void Error(string message)
        {
            errorText.text = LocalizationManager.GetWordByKey(message);
            loader.Hide();
        }
        
    }
}