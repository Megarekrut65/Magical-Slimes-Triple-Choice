using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Database;
using Firebase.Extensions;

namespace FightingMode.Lobby
{
    public abstract class RoomCreator
    {
        protected void CreateRoom(string roomType, UserInfo info, int maxHp, Action<bool,string> answer)
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;

            Guid g = Guid.NewGuid();
            FightingSaver.SaveCode(g.ToString());
            
            DatabaseReference room = db.RootReference.Child(roomType).Child(g.ToString());

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"creationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture)},
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
                FightingSaver.SaveRoomType(roomType);
                
                answer(true, "");
            });
        }
    }
}