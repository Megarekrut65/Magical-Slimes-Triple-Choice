﻿using System;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

namespace LoginRegister
{
    public static class RegisterController
    {
        public static void Register(string username, string email, string password, Action<bool, string> answer)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            auth.CreateUserWithEmailAndPasswordAsync(email, password)
                .ContinueWithOnMainThread(task => {
                    Debug.Log(task.Exception?.Message);
                    
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
                            Debug.Log(t.Exception?.Message);
                            
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