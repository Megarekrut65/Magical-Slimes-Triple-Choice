using System.Collections;
using Database;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
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
                    Debug.LogError(task.Exception.Message);
                    return;
                }
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
                db.Settings.PersistenceEnabled = false;
                
                FirebaseDatabase fb = FirebaseDatabase.DefaultInstance;
                fb.SetPersistenceEnabled(false);
                
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
                Debug.Log(task.Exception?.Message);
                
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
            CurrentVersion = Version.Current;
        }
    }
}