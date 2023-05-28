using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Auth;
using Global;
using JetBrains.Annotations;
using UnityEngine;

namespace DataManagement
{
    /// <summary>
    /// Syncs user data. If there is new instance of user data in database then loads it to device.
    /// Else shares device user data to database.
    /// </summary>
    public class DataSync
    {
        [CanBeNull] private string _userId;
        private Action<bool, string> _answer = (_, _) => {};
        
        public void SyncAllData(Action<bool, string> answer)
        {
            _answer = answer;
            
            FirebaseUser user = FirebaseManager.Auth.CurrentUser;
            _userId = user.UserId;

            if (_userId == null)
            {
                _answer(false, "");
                return;
            }
          
            UserData.GetUserDataFromDatabase(_userId, LoadData);
        }
        
        private void LoadData(bool result, Dictionary<string, object> data)
        {
            if (!result)
            {
                _answer(false, "");
                return;
            }

            DateTime dateTime = DateTime.Parse(data["lastSave"] as string, CultureInfo.InvariantCulture);
            DateTime savedDateTime = DataSaver.LoadLastSave();
            
            if (savedDateTime > dateTime)
            {
                CustomLogger.Log("Save");
                DatabaseSaver saver = new DatabaseSaver();
                saver.SaveUserData(_userId, _answer);
                return;
            }
            CustomLogger.Log("Load");
            DatabaseLoader loader = new DatabaseLoader();
            loader.LoadData(data);
            _answer(true, "");
        }
    }
}