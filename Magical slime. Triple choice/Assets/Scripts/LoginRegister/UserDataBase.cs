using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using UnityEngine;

namespace LoginRegister
{
    public static class UserDataBase
    {
        public static void AddNewUser(string id, string username, Action<bool, string> answer)
        {
            Debug.Log(id);
            Debug.Log(username);
            FirebaseFirestore db = FirebaseFirestore.GetInstance(FirebaseApp.Create());;
            Debug.Log("DB");
            DocumentReference docRef = db.Collection("users").Document("default");
            Debug.Log("Document");
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "username", username },
                //{ "maxLevel", DataSaver.LoadMaxLevelForAccount() },
                //{ "maxEnergy", DataSaver.LoadMaxEnergyForAccount()  },
                //{ "registrationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture) }//,
                //{ "slimes", DataSaver.LoadSlimeDataJson()  },
                //{ "diamonds", DataSaver.LoadDiamonds()  }
            };
            /*docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                answer(task.IsCompletedSuccessfully, task.Exception?.ToString() ?? "");
            });*/
        }
    }
}