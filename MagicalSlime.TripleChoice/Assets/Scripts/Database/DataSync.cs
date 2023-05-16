﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Auth;
using Global;
using JetBrains.Annotations;
using UnityEngine;

namespace Database
{
    public class DataSync
    {
        [CanBeNull] private string _userId;
        private Action<bool, string> _answer = (_, _) => {};
        
        public void SyncAllData(Action<bool, string> answer)
        {
            _answer = answer;
            
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
            _userId = user.UserId;
            _answer(false, "");
            if (user.UserId == null) return;
          
            UserData.GetUserDataFromDatabase(user.UserId, LoadData);
        }
        
        private void LoadData(bool result, Dictionary<string, object> data)
        {
            Debug.Log("Result: " + result);
            
            if (!result)
            {
                _answer(false, "");
                return;
            }
            
            DateTime dateTime = DateTime.Parse(data["lastSave"] as string, CultureInfo.InvariantCulture);
            DateTime savedDateTime = DataSaver.LoadLastSave();

            if (savedDateTime > dateTime)
            {
                if (_userId == null)
                {
                    _answer(false, "");
                    return;
                }

                Debug.Log("Save");
                DatabaseSaver saver = new DatabaseSaver();
                saver.SaveUserData(_userId, _answer);
                return;
            }
            Debug.Log("Load");
            DatabaseLoader loader = new DatabaseLoader();
            loader.LoadData(data);
            _answer(true, "");
        }
    }
}