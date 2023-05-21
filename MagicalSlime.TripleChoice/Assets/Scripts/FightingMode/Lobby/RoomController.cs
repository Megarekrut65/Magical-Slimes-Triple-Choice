using System;
using FightingMode.Lobby.Global;
using FightingMode.Lobby.Private;
using JetBrains.Annotations;

namespace FightingMode.Lobby
{
    public static class RoomController
    {
        [CanBeNull] private static GlobalRoomCreator _creator;
        public static void AddGlobalRoom(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            if(_creator is { IsCreated: true }) return;
            
            _creator = new GlobalRoomCreator(info, answer);
            _creator.Create(maxHp);
        }

        public static void Back(Action answer)
        {
            if (_creator is { IsCreated: true })
            {
                _creator.RemoveRoom(answer);
            }

            answer();
        }

        public static void AddPrivateRoom(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            PrivateRoomCreator creator = new PrivateRoomCreator();
            creator.Create(info, maxHp, answer);
        }

        public static void ConnectToGlobalRoom(UserInfo info, bool fast, Action<bool, string> answer)
        {
            GlobalRoomConnector connector = new GlobalRoomConnector(info, answer, fast);
            connector.Connect();
        }

        public static void ConnectToPrivateRoom(UserInfo info, string code, Action<bool, string> answer)
        {
            PrivateRoomConnector connector = new PrivateRoomConnector(info, code, answer);
            connector.Connect();
        }
    }
}