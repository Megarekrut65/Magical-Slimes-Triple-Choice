using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

namespace Fighting.Lobby
{
    public static class RoomController
    {
        public static void AddGlobalRoom(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            GlobalRoomCreator creator = new GlobalRoomCreator(info, answer);
            creator.Create(maxHp);
        }

        public static void AddPrivateRoom(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            PrivateRoomCreator creator = new PrivateRoomCreator();
            creator.Create(info, maxHp, answer);
        }

        public static void ConnectToGlobalRoom(UserInfo info, bool fast, Action<bool, string> answer)
        {
            
        }

        public static void ConnectToPrivateRoom(UserInfo info, string code, Action<bool, string> answer)
        {
            PrivateRoomConnector connector = new PrivateRoomConnector(info, code, answer);
            connector.Connect();
        }
    }
}