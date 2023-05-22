using System;
using System.Collections.Generic;
using System.Globalization;
using DataManagement;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;

namespace LoginRegister
{
    /// <summary>
    /// Controls adding data of new users and getting data of old one.
    /// </summary>
    public static class UserController
    {
        public static void AddNewUser(string id, string username, Action<bool, string> answer)
        {
            FirebaseFirestore db = FirebaseManager.Fs;

            DocumentReference docRef = db.Collection("users").Document(id);

            Dictionary<string, object> data = UserData.GetUserDataFromLocalStorage();

            data.Add("username", username);
            data.Add("registrationDate", DateTimeUtc.NowInvariant);

            docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                DatabaseLoader loader = new DatabaseLoader();
                loader.LoadData(data);
                answer(task.IsCompletedSuccessfully, task.Exception?.Message ?? "");
            });
        }

        /// <summary>
        /// Gets user data from database and checks conflicts.
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="answer">answer after getting database result</param>
        /// <param name="conflict">action to remove conflicts of local and database data</param>
        public static void AuthorizeUser(string id, Action<bool, string> answer, 
            Action<Dictionary<string, object>> conflict)
        {
            UserData.GetUserDataFromDatabase(id, (ans, data) =>
            {
                if (!ans || data == null)
                {
                    answer(false, "some-error-database");
                    return;
                }

                if (Difference.IsDifference(data))
                {
                    answer(false, "");
                    conflict(data);
                    return;
                }
                DatabaseLoader loader = new DatabaseLoader();
                loader.LoadData(data);
                answer(true, "");
            });
        }
    }
}