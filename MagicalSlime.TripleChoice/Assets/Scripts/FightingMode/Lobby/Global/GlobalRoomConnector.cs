using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using Global;
using UnityEngine;

namespace FightingMode.Lobby.Global
{
    public class GlobalRoomConnector: RoomConnector
    {
        private string _code;
        private bool _fast;
        
        public GlobalRoomConnector(UserInfo info, Action<bool, string> answer, bool fast) : 
            base(info, answer, "global-rooms")
        {
            _fast = fast;
        }

        public override void Connect()
        {
            _code = "";
            
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference free = db.RootReference.Child("global-free");

            int count = 0;
            free.RunTransaction(data =>
            {
                if (!data.HasChildren)
                {
                    answer(false, count > 0? "room-not-found":"");
                    count++;
                    return TransactionResult.Success(data);
                }

                foreach (MutableData snapshot in data.Children)
                {
                    bool isFree = Convert.ToBoolean(snapshot.Child("isFree").Value);
                    if (isFree)
                    {
                        _code = snapshot.Key;
                        snapshot.Child("isFree").Value = false;
                        break;
                    }
                }

                return TransactionResult.Success(data);
            }).ContinueWithOnMainThread(MakeRoomNotFree);
        }

        private void MakeRoomNotFree(Task<DataSnapshot> task)
        {
            if (_code == "")
            {
                Debug.Log("Room not found!");
                answer(false, "room-not-found");
                return;
            }
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            
            if (task.IsCompletedSuccessfully)
            {
                DatabaseReference room = db.RootReference.Child("global-free").Child(_code);
                room.RemoveValueAsync();
            }
            
            FightingSaver.SaveCode(_code);
            DatabaseReference globalRoom = db.RootReference.Child("global-rooms").Child(_code);
            globalRoom.Child("client").SetValueAsync(info.ToDictionary()).ContinueWithOnMainThread(t =>
            {
                if (t.IsFaulted)
                {
                    CustomLogger.Log(t.Exception?.Message);
                    answer(false, "some-error-database");
                    return;
                }

                globalRoom.GetValueAsync().ContinueWithOnMainThread(base.SaveRoomData);
            });
        }
    }
}