using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database;
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
        public string CurrentVersion { get; private set; } = "";

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
                
                LoadVersion(db);
            });
        }

        private void LoadVersion(FirebaseFirestore db)
        {
            db.Collection("system")
                .Document("version")
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    CurrentVersion = task.Result.GetValue<string>("current");

                    LoadUser();
                    return;
                }

                CurrentVersion = Version.Current;
                LoadUser();
            });
        }
        private void LoadUser()
        {
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            if (user != null)
            {
                DataSync dataSync = new DataSync();
                dataSync.SyncAllData((_,_)=>Ready = true);
            }
            Ready = true;
        }
        private IEnumerator Waiter()
        {
            yield return new WaitForSeconds(10f);
            Ready = true;
        }
    }
}