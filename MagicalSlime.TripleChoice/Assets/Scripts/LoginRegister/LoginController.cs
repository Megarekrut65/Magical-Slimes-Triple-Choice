using System;
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
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
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
            return FirebaseAuth.DefaultInstance.CurrentUser != null;
        }
        public static void SignOutUser() {
            FirebaseAuth.DefaultInstance.SignOut();
        }
        public static void ResetPassword(string email, Action<bool, string> answer) {
            var auth = FirebaseAuth.DefaultInstance;
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