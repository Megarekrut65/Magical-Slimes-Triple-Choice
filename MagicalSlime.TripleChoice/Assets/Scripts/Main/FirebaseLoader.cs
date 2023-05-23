using System.Collections;
using DataManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using UnityEngine;

namespace Main
{
    /// <summary>
    /// Loads database dependencies
    /// </summary>
    public class FirebaseLoader : MonoBehaviour
    {
        public bool Ready { get; private set; } = false;
        public string MinRequiredVersion { get; private set; } = "";

        private void Start()
        {
            StartCoroutine(Waiter());
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                if (task.Exception != null) {
                    CustomLogger.Log(task.Exception.Message);
                    return;
                }

                FirebaseFirestore db = FirebaseManager.Fs;
                db.Settings.PersistenceEnabled = false;

                FirebaseDatabase fb = FirebaseManager.Db;
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
                CustomLogger.Log(task.Exception?.Message);
                
                if (task.IsCompletedSuccessfully)
                {
                    MinRequiredVersion = task.Result.GetValue<string>("required");

                    LoadUser();
                    return;
                }

                MinRequiredVersion = Version.Current;
                LoadUser();
            });
        }
        private void LoadUser()
        {
            FirebaseUser user = FirebaseManager.Auth.CurrentUser;
            if (user != null)
            {
                if (DataSaver.LoadUsername() == "")
                {
                    FirebaseManager.Auth.SignOut();
                    Ready = true;
                    return;
                }
                DataSync dataSync = new DataSync();
                dataSync.SyncAllData((_,_)=>Ready = true);
            }
            Ready = true;
        }
        private IEnumerator Waiter()
        {
            yield return new WaitForSeconds(10f);
            Ready = true;
            MinRequiredVersion = Version.Current;
        }
    }
}