using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Global;
using UnityEngine;

namespace FightingMode.Lobby
{
    public abstract class RoomConnector
    {
        protected readonly UserInfo info;
        protected Action<bool, string> answer;
        private readonly string _roomType;

        public RoomConnector(UserInfo info, Action<bool, string> answer, string roomType)
        {
            this.info = info;
            this.answer = answer;
            _roomType = roomType;
        }

        protected virtual void SaveRoomData(Task<DataSnapshot> task)
        {
            CustomLogger.Log(task.Exception?.Message);
            if (task.IsFaulted)
            {
                answer(false, "room-error");
                return;
            }

            FightingSaver.SaveFirst(task.Result.Child("first").Value as string);
            FightingSaver.SaveUserInfo("mainInfo", info);
                
            Dictionary<string, object> dictionary = task.Result.Child("host").Value as Dictionary<string, object>;

            FightingSaver.SaveUserInfo("enemyInfo", UserInfo.FromDictionary(dictionary));

            FightingSaver.SaveMainType("client");
            FightingSaver.SaveRoomType(_roomType);

            answer(true, "");
        }

        public abstract void Connect();
    }
}