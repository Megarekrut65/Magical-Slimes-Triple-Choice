using System;
using System.Globalization;
using System.Threading.Tasks;
using DataManagement;
using Firebase.Database;
using Firebase.Extensions;
using Global;
using UnityEngine;

namespace FightingMode.Lobby.Global
{
    public class GlobalRoomConnector: RoomConnector
    {
        private string _code;
        private readonly bool _fast;

        private const float MinDistance = 100f;
        private const float LevelPart = 0.5f;

        public GlobalRoomConnector(UserInfo info, Action<bool, string> answer, bool fast) : 
            base(info, answer, "global-rooms")
        {
            _fast = fast;
        }

        public override void Connect()
        {
            _code = "";
            Point main = new Point(info.cups, info.maxLevel);

            FirebaseDatabase db = FirebaseManager.Db;
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

                RoomSearcher searcher = new RoomSearcher(MinDistance, main, _fast, LevelPart);
                _code = searcher.Search(data);
                if (_code == "") return TransactionResult.Abort();
                
                data.Child(_code).Child("isFree").Value = false;

                return TransactionResult.Success(data);

            }).ContinueWithOnMainThread(MakeRoomNotFree);
        }
        
        private void MakeRoomNotFree(Task<DataSnapshot> task)
        {
            if (_code == "")
            {
                answer(false, "room-not-found");
                return;
            }

            FirebaseDatabase db = FirebaseManager.Db;
            
            if (task.IsCompletedSuccessfully)
            {
                DatabaseReference room = db.RootReference.Child("global-free").Child(_code);
                room.RemoveValueAsync();
            }
            
            FightingSaver.SaveCode(_code);
            DatabaseReference globalRoom = db.RootReference.Child("global-rooms").Child(_code);
            globalRoom.Child("hostAlive").GetValueAsync()
                .ContinueWithOnMainThread(t =>CheckHostIsAlive(globalRoom, t));
        }

        private void CheckHostIsAlive(DatabaseReference globalRoom, Task<DataSnapshot> t)
        {
            if (t.IsFaulted)
            {
                CustomLogger.Log(t.Exception?.Message);
                globalRoom.RemoveValueAsync();
                answer(false, "host-not-alive");
                return;
            }

            DateTime dateTime = Convert.ToDateTime(t.Result.Value as string, CultureInfo.InvariantCulture);
            DateTime now = DateTimeUtc.Now;

            if (dateTime - now > TimeSpan.FromSeconds(3) || dateTime - now < TimeSpan.FromSeconds(-3))
            {
                CustomLogger.Log("Host not alive");
                globalRoom.RemoveValueAsync();
                answer(false, "host-not-alive");
                return;
            }

            AddDataToRoom(globalRoom);
        }

        private void AddDataToRoom(DatabaseReference globalRoom)
        {
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