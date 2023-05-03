using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using UnityEngine;

namespace DataBase
{
    public static class UserData
    {
        public static void SendToDatabase(string id, Action<bool, string> answer)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            DocumentReference docRef = db.Collection("users").Document(id);

            docRef.SetAsync(GetUserData(), SetOptions.MergeAll).ContinueWithOnMainThread(task =>
            {
                answer(task.IsCompletedSuccessfully, task.Exception?.ToString() ?? "");
            });
        }

        public static Dictionary<string, object> GetUserData()
        {
            return new Dictionary<string, object>
            {
                { "maxLevel", DataSaver.LoadMaxLevelForAccount() },
                { "maxEnergy", DataSaver.LoadMaxEnergyForAccount()  },
                { "slimes", DataSaver.LoadSlimeDataJson()  },
                { "diamonds", DataSaver.LoadDiamonds()  }
            };
        }

        public static void GetUserDataFromDatabase(string id, Action<bool, Dictionary<string, object>> answer)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("users").Document(id);
            
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    answer(true, snapshot.ToDictionary());
                    return;
                }

                answer(false, null);
            });
        }
    }
}