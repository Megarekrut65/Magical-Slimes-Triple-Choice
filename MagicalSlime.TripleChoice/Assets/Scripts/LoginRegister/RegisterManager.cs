using DataManagement;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace LoginRegister
{
    public class RegisterManager : LoginManager
    {
        [SerializeField] private InputField usernameField;
        [SerializeField] private  InputField repeatPasswordField;

        public void Register()
        {
            Error("");
            string username = usernameField.text;
            string email = emailField.text;
            string password = passwordField.text;
            string repeatPassword = repeatPasswordField.text;

            if (username.Length < 4 || email.Length < 4 || password.Length < 8)
            {
                Error("input-length");
                return;
            }

            if (password != repeatPassword)
            {
                Error("dont-match");
                return;
            }
            RegisterController.Register(username, email, password, RegisterAnswer);
        }

        private void RegisterAnswer(bool success, string message)
        {
            if (success)
            {
                FirebaseAuth auth = FirebaseManager.Auth;
                if (auth.CurrentUser == null)
                {
                    Answer(false, "some-error-register");
                    return;
                }
                UserController.AddNewUser(auth.CurrentUser.UserId, auth.CurrentUser.DisplayName, Answer);
                return;
            }
            Answer(false, message);
        }
    }
}