using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Extensions;
using Firebase.Firestore;
using DataBase;
using Google.MiniJSON;
using UnityEngine;

namespace LoginRegister
{
    public static class UserController
    {
        public static void AddNewUser(string id, string username, Action<bool, string> answer)
        {
            DatabaseSaver saver = new DatabaseSaver();
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            DocumentReference docRef = db.Collection("users").Document(id);

            Dictionary<string, object> data = saver.GetAllData();
            data.Add("lastSave", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            data.Add("username", username);
            data.Add("registrationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture));

            docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                DatabaseLoader loader = new DatabaseLoader();
                loader.LoadData(data);
                answer(task.IsCompletedSuccessfully, task.Exception?.ToString() ?? "");
            });
        }

        public static void AuthorizeUser(string id, Action<bool, string> answer)
        {
            UserData.GetUserDataFromDatabase(id, (ans, data) =>
            {
                if (!ans || data == null)
                {
                    answer(false, "");
                    return;
                }
                DatabaseLoader loader = new DatabaseLoader();
                loader.LoadData(data);
            });
        }
    }
}