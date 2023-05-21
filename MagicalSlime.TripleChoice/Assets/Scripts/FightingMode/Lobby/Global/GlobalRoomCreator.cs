using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

namespace FightingMode.Lobby.Global
{
    public class GlobalRoomCreator: RoomCreator
    {
        public bool IsCreated { get; private set; }
        private string _code;
        
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

            _code = FightingSaver.LoadCode();
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference.Child("global-free").Child(_code);
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"cups", _info.cups},
                {"maxLevel", _info.maxLevel},
                {"isFree", true},
                {"creationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture)}
            };

            room.SetValueAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    _answer(false, "fail-create-room");
                    return;
                }
                IsCreated = true;
                AddHandlingConnection();
            });
        }

        private void AddHandlingConnection()
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference.Child("global-rooms").Child(_code);
            room.Child("client").ValueChanged += EnemyConnectHandler;
        }

        private void EnemyConnectHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists || !args.Snapshot.HasChild("client")) return;
            UserInfo enemy = UserInfo.FromDictionary(args.Snapshot.Value as Dictionary<string, object>);
            
            FightingSaver.SaveUserInfo("enemyInfo", enemy);

            _answer(true, "");
        }

        public void RemoveRoom(Action answer)
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference roomFree = db.RootReference.Child("global-free").Child(_code);
            roomFree.RemoveValueAsync().ContinueWithOnMainThread(_ =>
            {
                DatabaseReference roomGlobal = db.RootReference.Child("global-rooms").Child(_code);
                roomGlobal.RemoveValueAsync().ContinueWithOnMainThread(_ =>
                {
                    answer();
                });
            });
        }
    }
}