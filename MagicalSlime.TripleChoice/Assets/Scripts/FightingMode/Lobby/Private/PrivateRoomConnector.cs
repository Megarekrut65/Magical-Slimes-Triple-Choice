using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

namespace FightingMode.Lobby.Private
{
    public class PrivateRoomConnector
    {
        private readonly UserInfo _info;
        private readonly string _code;
        private Action<bool, string> _answer;

        public PrivateRoomConnector(UserInfo info, string code, Action<bool, string> answer)
        {
            _info = info;
            _code = code;
            _answer = answer;
        }
        
        public void Connect()
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference.Child("private-rooms").Child(_code);
            
            int count = 0;
            room.RunTransaction(data =>
                {
                    if (!data.HasChildren)
                    {
                        data.Value = new Dictionary<string, object>();
                        _answer(false, count > 0? "room-not-found":"");
                        count++;
                        return TransactionResult.Success(data);
                    }
                    if (data.HasChild("client"))
                    {
                        _answer(false,  "room-full");
                        _answer = (_, _) => { };
                        return TransactionResult.Abort();
                    }

                    data.Child("client").Value = _info.ToDictionary();

                    return TransactionResult.Success(data);
                })
                .ContinueWithOnMainThread(SaveRoomData);
        }

        private void SaveRoomData(Task<DataSnapshot> task)
        {
            Debug.Log(task.Exception?.Message);
            if (task.IsFaulted)
            {
                _answer(false, "room-error");
                return;
            }
            FightingSaver.SaveCode(_code);
            FightingSaver.SaveMaxHp(Convert.ToInt32(task.Result.Child("maxHp").Value));
            FightingSaver.SaveUserInfo("mainInfo", _info);
                
            Dictionary<string, object> dictionary = task.Result.Child("host").Value as Dictionary<string, object>;

            FightingSaver.SaveUserInfo("enemyInfo", UserInfo.FromDictionary(dictionary));

            FightingSaver.SaveMainType("client");
            FightingSaver.SaveRoomType("private-rooms");

            _answer(true, "");
        }
    }
}