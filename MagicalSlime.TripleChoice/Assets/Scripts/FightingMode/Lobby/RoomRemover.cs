using System;
using System.Collections.Generic;
using System.Globalization;
using DataManagement;
using Firebase.Database;
using Firebase.Extensions;
using Global;
using UnityEngine;

namespace FightingMode.Lobby
{
    public static class RoomRemover
    {
        public static void RemoveOld(string type)
        {
            FirebaseDatabase db = FirebaseManager.Db;
            DatabaseReference rooms = db.RootReference.Child("global-rooms");
            List<string> codes = new List<string>();
            rooms.GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    CustomLogger.Log(task.Exception?.Message);
                    return;
                }

                foreach (DataSnapshot data in task.Result.Children)
                {
                    if (!data.Exists || !data.HasChild("hostAlive"))
                    {
                        codes.Add(data.Key);
                        continue;
                    }
                    DateTime dateTime = Convert.ToDateTime(data.Child("hostAlive").Value as string, 
                        CultureInfo.InvariantCulture);
                    DateTime now = DateTimeUtc.Now;

                    if (dateTime - now > TimeSpan.FromDays(1) || dateTime - now < TimeSpan.FromDays(-1))
                    {
                        codes.Add(data.Key);
                    }
                }

                RemoveAll(rooms, codes);
            });
        }

        private static void RemoveAll(DatabaseReference rooms, List<string> codes)
        {
            foreach (string code in codes)
            {
                rooms.Child(code).RemoveValueAsync();
            }
        }
    }
}