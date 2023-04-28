using System;
using Firebase.Auth;
using Firebase.Extensions;

namespace LoginRegister
{
    public static class RegisterController
    {
        public static void Register(string username, string email, string password, Action<bool, string> answer)
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
                if (task.IsCanceled) {
                    answer(false, "some-error-register");
                    return;
                }

                if (task.IsFaulted) {
                    answer(false, "already-exists");
                    return;
                }

                FirebaseUser newUser = task.Result;
                newUser.UpdateUserProfileAsync(new UserProfile {DisplayName = username})
                    .ContinueWithOnMainThread(t => {
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