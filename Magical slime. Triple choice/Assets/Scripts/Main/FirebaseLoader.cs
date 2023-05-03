using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

namespace Main
{
    /// <summary>
    /// Loads database dependencies
    /// </summary>
    public class FirebaseLoader : MonoBehaviour
    {
        public bool Ready { get; private set; } = false;

        private void Start()
        {
            StartCoroutine(Waiter());
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Exception != null) {
                    Debug.LogError(task.Exception);
                    return;
                }
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
                db.Settings.PersistenceEnabled = false;

                FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
                if (user != null)
                {
                    DataSync dataSync = new DataSync();
                    dataSync.SyncAllData((_,_)=>Ready = true);
                    return;
                }
                Ready = true;
            });
        }

        private IEnumerator Waiter()
        {
            yield return new WaitForSeconds(10f);
            Ready = true;
        }
    }
}