using System;
using DataManagement;
using Firebase.Auth;
using Firebase.Extensions;
using Global;
using UnityEngine;

namespace LoginRegister
{
    public static class RegisterController
    {
        public static void Register(string username, string email, string password, Action<bool, string> answer)
        {
            FirebaseAuth auth = FirebaseManager.Auth;
            auth.CreateUserWithEmailAndPasswordAsync(email, password)
                .ContinueWithOnMainThread(task => {
                    CustomLogger.Log(task.Exception?.Message);
                    
                    if (task.IsCanceled) {
                        answer(false, "some-error-register");
                        return;
                    }

                    if (task.IsFaulted) {
                        answer(false, "already-exists");
                        return;
                    }

                    FirebaseUser newUser = task.Result.User;
                    newUser.UpdateUserProfileAsync(new UserProfile {DisplayName = username})
                        .ContinueWithOnMainThread(t => {
                            CustomLogger.Log(t.Exception?.Message);
                            
                            if (t.Exception != null) {
                                answer(false, "some-error-register");
                                return;
                            }
                            answer(true, "");
                        });
                });
        }
    }
}