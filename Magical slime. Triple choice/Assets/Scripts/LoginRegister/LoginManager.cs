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
            string email = emailField.text;
            string password = passwordField.text;
            
            LoginController.Login(email, password, Answer);
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