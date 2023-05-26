using System.Collections.Generic;
using DataManagement;
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

        protected bool _isClicked;

        public void Login()
        {
            if(_isClicked) return;
            _isClicked = true;
            
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
                FirebaseAuth auth = FirebaseManager.Auth;
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
            conflictManager.Conflict(data, UserData.GetUserDataFromLocalStorage());
        }
        protected void Answer(bool success, string message)
        {
            _isClicked = false;
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