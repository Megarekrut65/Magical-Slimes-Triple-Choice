using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

namespace Main
{
    /// <summary>
    /// Loads database dependencies
    /// </summary>
    public class FirebaseLoader : MonoBehaviour
    {
        public bool Ready { get; private set; } = false;

        private void Start() {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Exception != null) {
                    Debug.LogError(task.Exception);
                    return;
                }
                
                Ready = true;
            });
        }
    }
}