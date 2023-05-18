using System;

namespace FightingMode.Lobby.Private
{
    public class PrivateRoomCreator: RoomCreator
    {
        public void Create(UserInfo info, int maxHp, Action<bool, string> answer)
        {
            CreateRoom("private-rooms", info, maxHp, answer);
        }
    }
}