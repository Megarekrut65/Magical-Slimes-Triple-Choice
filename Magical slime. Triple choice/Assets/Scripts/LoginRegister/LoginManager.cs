using DataBase;
using Firebase.Auth;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LoginRegister
{
    public class LoginManager : MonoBehaviour
    {
        [SerializeField] private Text errorText;
        
        [SerializeField] protected InputField emailField;
        [SerializeField] protected InputField passwordField;
        
        public void Login()
        {
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
                
                UserController.AuthorizeUser(auth.CurrentUser.UserId, Answer);
                return;
            }
            Answer(false, message);
        }
        protected void Answer(bool success, string message)
        {
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
        }
        
    }
}