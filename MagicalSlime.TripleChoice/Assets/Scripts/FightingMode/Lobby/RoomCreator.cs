using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Database;
using Firebase.Extensions;
using Random = UnityEngine.Random;

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

            string first = Random.Range(0, 100) >= 50 ? "host" : "client"; 
            
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"creationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture)},
                {"maxHp", maxHp},
                {"host", info.ToDictionary()},
                {"first", first}
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
                FightingSaver.SaveFirst(first);
                FightingSaver.SaveMaxHp(maxHp);
                
                answer(true, "");
            });
        }
    }
}