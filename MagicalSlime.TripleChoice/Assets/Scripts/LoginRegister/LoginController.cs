using System;
using DataManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Global;
using UnityEngine;

namespace LoginRegister
{
    public static class LoginController
    {
        public static void Login(string email, string password, Action<bool, string> answer)
        {
            FirebaseAuth auth = FirebaseManager.Auth;
            auth.SignInWithEmailAndPasswordAsync(email, password)
                .ContinueWithOnMainThread(task => {
                    CustomLogger.Log(task.Exception?.Message);
                    if (task.IsFaulted) {
                        answer(false, "doesnt-exist");
                        return;
                    }

                    answer(true, "");
                });
        }
        public static bool UserSignIn() {
            return FirebaseManager.Auth.CurrentUser != null;
        }
        public static void SignOutUser() {
            FirebaseManager.Auth.SignOut();
        }
        public static void ResetPassword(string email, Action<bool, string> answer) {
            FirebaseAuth auth = FirebaseManager.Auth;
            auth.SendPasswordResetEmailAsync(email)
                .ContinueWithOnMainThread(task => {
                    CustomLogger.Log(task.Exception?.Message);
                    if (task.Exception != null) {
                        answer(false, "invalid-email");
                        return;
                    }

                    answer(true, "");
                });
        }
    }
}