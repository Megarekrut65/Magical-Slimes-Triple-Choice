using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;

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
                {"maxLevel", _info.maxLevel}
            };

            room.SetValueAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    _answer(false, "fail-create-room");
                    return;
                }

                _answer(true, "");
            });
        }
    }
}