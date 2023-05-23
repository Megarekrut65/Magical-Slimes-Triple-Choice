using System;
using System.Collections.Generic;
using DataManagement;
using FightingMode.Game.Choice;
using Firebase.Database;
using Firebase.Extensions;
using Global;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightingMode.Lobby
{
    public abstract class RoomCreator
    {
        protected void CreateRoom(string roomType, UserInfo info, int maxHp, Action<bool,string> answer)
        {
            FirebaseDatabase db = FirebaseManager.Db;

            Guid g = Guid.NewGuid();
            FightingSaver.SaveCode(g.ToString());
            DatabaseReference room = db.RootReference.Child(roomType).Child(g.ToString());

            string first = Random.Range(0, 100) >= 50 ? "host" : "client";
            int defaultChoiceHost = Random.Range(0, 3);
            int defaultChoiceClient = Random.Range(0, 3);
            while (defaultChoiceClient == defaultChoiceHost) defaultChoiceClient = Random.Range(0, 3);

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"creationDate", DateTimeUtc.NowInvariant},
                {"maxHp", maxHp},
                {"host", info.ToDictionary()},
                {"first", first},
                {"defaultChoiceHost", defaultChoiceHost},
                {"defaultChoiceClient", defaultChoiceClient},
                {"hostAlive", DateTimeUtc.NowInvariant}
            };

            room.SetValueAsync(data).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    CustomLogger.Log(task.Exception?.Message);
                    
                    answer(false, "fail-create-room");
                    return;
                }
                FightingSaver.SaveUserInfo("mainInfo", info);
                FightingSaver.SaveMainType("host");
                FightingSaver.SaveRoomType(roomType);
                FightingSaver.SaveFirst(first);
                FightingSaver.SaveMaxHp(maxHp);
                
                FightingSaver.SaveDefaultChoice("host", (ChoiceType)defaultChoiceHost);
                FightingSaver.SaveDefaultChoice("client", (ChoiceType)defaultChoiceClient);
                
                answer(true, "");
            });
        }
    }
}