using System;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using UnityEngine;

namespace Database
{
    public static class UserData
    {
        public static void GetUserDataFromDatabase(string id, Action<bool, Dictionary<string, object>> answer)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("users").Document(id);
            Debug.Log(id);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                Debug.Log(task.Exception?.Message);
                if (task.IsFaulted)
                {
                    answer(false, null);
                    return;
                }
                DocumentSnapshot snapshot = task.Result;
                Debug.Log(task.Result);
                Debug.Log(JsonUtility.ToJson(task.Result));
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