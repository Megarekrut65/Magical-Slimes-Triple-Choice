using System;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;

namespace Fighting.Lobby
{
    public class RoomController
    {
        public static void AddGlobalRoom(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            AddRoom("global-rooms", info, maxHp, (success, message) =>
            {
                if (!success)
                {
                    answer(false, message);
                    return;
                }
                
                FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
                DatabaseReference room = db.RootReference.Child("global-free").Child(FightingSaver.LoadCode());
                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    {"cups", info.cups},
                    {"maxLevel", info.maxLevel}
                };

                room.SetValueAsync(data).ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted)
                    {
                        answer(false, "fail-create-room");
                        return;
                    }

                    answer(true, "");
                });
            });
        }

        public static void AddPrivateRoom(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            AddRoom("private-rooms", info, maxHp, answer);
        }
        private static void AddRoom(string roomType, UserInfo info, int maxHp, Action<bool,string> answer)
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            
            Guid g = Guid.NewGuid();
            FightingSaver.SaveCode(g.ToString());
            
            DatabaseReference room = db.RootReference.Child(roomType).Child(g.ToString());

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"maxHp", maxHp},
                {"host", info.ToDictionary()}
            };

            room.SetValueAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    answer(false, "fail-create-room");
                    return;
                }
                FightingSaver.SaveUserInfo("mainInfo", info);
                FightingSaver.SaveMainType("host");
                
                answer(true, "");
            });
        }

        public void ConnectToGlobalRoom(UserInfo info, bool fast, Action<bool, string> answer)
        {
            
        }

        public void ConnectToPrivateRoom(UserInfo info, string code, Action<bool, string> answer)
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;

            DatabaseReference room = db.RootReference.Child("private-rooms").Child(code);
            room.RunTransaction(data =>
            {
                if (!data.HasChildren || data.HasChild("client"))
                {
                    answer(false,  data.HasChild("client")?"room-full":"room-not-found");
                    return TransactionResult.Abort();
                }

                data.Child("client").Value = info;
                
                FightingSaver.SaveCode(code);
                FightingSaver.SaveMaxHp(Convert.ToInt32(data.Child("maxHp").Value));
                FightingSaver.SaveUserInfo("mainInfo", info);
                
                Dictionary<string, object> dictionary = data.Child("client").Value as Dictionary<string, object>;
                FightingSaver.SaveUserInfo("enemyInfo", UserInfo.FromDictionary(dictionary));
                
                FightingSaver.SaveMainType("client");
                
                return TransactionResult.Success(data);
            });
        }
    }
}