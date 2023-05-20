using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

namespace FightingMode.Lobby.Private
{
    public class PrivateRoomConnector: RoomConnector
    {
        private readonly string _code;

        public PrivateRoomConnector(UserInfo info, string code, Action<bool, string> answer): 
            base(info, answer, "private-rooms")
        {
            _code = code;
        }
        
        public override void Connect()
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference.Child("private-rooms").Child(_code);
            
            int count = 0;
            room.RunTransaction(data =>
                {
                    if (!data.HasChildren)
                    {
                        data.Value = new Dictionary<string, object>();
                        answer(false, count > 0? "room-not-found":"");
                        count++;
                        return TransactionResult.Success(data);
                    }
                    if (data.HasChild("client"))
                    {
                        answer(false,  "room-full");
                        answer = (_, _) => { };
                        return TransactionResult.Abort();
                    }

                    data.Child("client").Value = info.ToDictionary();

                    return TransactionResult.Success(data);
                })
                .ContinueWithOnMainThread(SaveRoomData);
        }

        protected override void SaveRoomData(Task<DataSnapshot> task)
        {
            FightingSaver.SaveCode(_code);
            FightingSaver.SaveMaxHp(Convert.ToInt32(task.Result.Child("maxHp").Value));
            base.SaveRoomData(task);
        }
    }
}