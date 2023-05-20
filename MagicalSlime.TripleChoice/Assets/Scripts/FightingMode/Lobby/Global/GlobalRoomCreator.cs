using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

namespace FightingMode.Lobby.Global
{
    public class GlobalRoomCreator: RoomCreator
    {

        private readonly UserInfo _info;
        private readonly Action<bool, string> _answer;
        
        public GlobalRoomCreator(UserInfo info, Action<bool, string> answer)
        {
            _info = info;
            _answer = answer;
        }

        public void Create(int maxHp)
        {
            CreateRoom("global-rooms", _info, maxHp, AddToFreeRooms);
        }

        private void AddToFreeRooms(bool success, string message)
        {
            if (!success)
            {
                _answer(false, message);
                return;
            }
                
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference.Child("global-free").Child(FightingSaver.LoadCode());
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"cups", _info.cups},
                {"maxLevel", _info.maxLevel},
                {"isFree", true}
            };

            room.SetValueAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    _answer(false, "fail-create-room");
                    return;
                }
                
                AddHandlingConnection();
            });
        }

        private void AddHandlingConnection()
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference.Child("global-rooms").Child(FightingSaver.LoadCode());
            room.Child("client").ValueChanged += EnemyConnectHandler;
        }

        private void EnemyConnectHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists || !args.Snapshot.HasChild("client")) return;
            UserInfo enemy = UserInfo.FromDictionary(args.Snapshot.Value as Dictionary<string, object>);
            
            FightingSaver.SaveUserInfo("enemyInfo", enemy);

            _answer(true, "");
        }
    }
}