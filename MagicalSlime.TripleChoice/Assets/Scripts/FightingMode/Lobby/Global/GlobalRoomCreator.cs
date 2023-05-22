using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DataManagement;
using Firebase.Database;
using Firebase.Extensions;
using Global;
using JetBrains.Annotations;
using UnityEngine;

namespace FightingMode.Lobby.Global
{
    public class GlobalRoomCreator: RoomCreator
    {
        public bool IsCreated { get; private set; }
        private string _code;
        
        private readonly UserInfo _info;
        private readonly Action<bool, string> _answer;
        
        private readonly FirebaseDatabase _db;
        [CanBeNull] private IEnumerator _keepAlive;
        
        public GlobalRoomCreator(UserInfo info, Action<bool, string> answer)
        {
            _info = info;
            _answer = answer;
            _db = FirebaseManager.Db;
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
            DatabaseReference room = _db.RootReference.Child("global-free").Child(_code);

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                {"cups", _info.cups},
                {"maxLevel", _info.maxLevel},
                {"isFree", true},
                {"creationDate", DateTimeUtc.NowInvariant}
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
                _keepAlive = KeepAlive();
                CoroutineStarter.StartOne(_keepAlive);
            });
            room.ValueChanged += RoomRemoved;
        }

        private IEnumerator KeepAlive()
        {
            DatabaseReference globalRoom = _db.RootReference.Child("global-rooms").Child(_code).Child("hostAlive");
            while (Thread.CurrentThread.IsAlive)
            {
                yield return new WaitForSeconds(1f);
                globalRoom.SetValueAsync(DateTimeUtc.NowInvariant);
            }
        }

        private void AddHandlingConnection()
        {
            DatabaseReference room = _db.RootReference.Child("global-rooms").Child(_code);
            room.Child("client").ValueChanged += EnemyConnectHandler;
        }

        private void RoomRemoved(object sender, ValueChangedEventArgs args)
        {
            if (args.Snapshot.Exists && args.Snapshot.ChildrenCount >= 2) return;
            CoroutineStarter.StopOne(_keepAlive);
            _answer(false, "try-again");
        }
        private void EnemyConnectHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists) return;
            CoroutineStarter.StopOne(_keepAlive);
            
            UserInfo enemy = UserInfo.FromDictionary(args.Snapshot.Value as Dictionary<string, object>);
            
            FightingSaver.SaveUserInfo("enemyInfo", enemy);

            _answer(true, "");
        }

        public void RemoveRoom(Action answer)
        {
            CoroutineStarter.StopOne(_keepAlive);
            DatabaseReference roomFree = _db.RootReference.Child("global-free").Child(_code);
            roomFree.RemoveValueAsync().ContinueWithOnMainThread(_ =>
            {
                DatabaseReference roomGlobal = _db.RootReference.Child("global-rooms").Child(_code);
                roomGlobal.RemoveValueAsync().ContinueWithOnMainThread(_ =>
                {
                    answer();
                });
            });
        }
    }
}