using System;
using System.Collections.Generic;
using System.Globalization;
using FightingMode;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using Global.Hats;
using IncrementalMode.AutoFarming;
using IncrementalMode.Shop;
using UnityEngine;

namespace DataManagement
{
    /// <summary>
    /// Saves user data to database
    /// </summary>
    public class DatabaseSaver
    {
        public void SaveUserData(string id, Action<bool, string> answer)
        {
            FirebaseFirestore db = FirebaseManager.Fs;

            DocumentReference docRef = db.Collection("users").Document(id);

            docRef.SetAsync(UserData.GetUserDataFromLocalStorage(), SetOptions.MergeAll)
                .ContinueWithOnMainThread(task =>
                {
                    CustomLogger.Log(task.Exception?.Message);
                    answer(task.IsCompletedSuccessfully, task.Exception?.Message ?? "");
                });
        }
    }
}