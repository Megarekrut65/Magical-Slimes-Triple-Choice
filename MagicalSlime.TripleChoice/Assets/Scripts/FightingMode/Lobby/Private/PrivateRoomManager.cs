using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FightingMode.Lobby.Private
{
    public class PrivateRoomManager : RoomManager
    {
        [SerializeField] private InputField maxHp;
        
        [SerializeField] private InputField codeInput;
        
        
        public void CreateRoom()
        {
            if(isClicked) return;
            
            Click();
            int hp = Convert.ToInt32(maxHp.text);
            hp = Math.Max(50, hp);
            hp = Math.Min(hp, 999);
            RoomController.AddPrivateRoom(UserInfoTaker.Take(), hp, Answer);
        }

        public void ConnectToRoom()
        {
            if(isClicked) return;
            Click();
            
            RoomController.ConnectToPrivateRoom(UserInfoTaker.Take(), codeInput.text, Answer);
        }

        private void Answer(bool success, string message)
        {
            if (success)
            {
                SceneManager.LoadScene("PrivateLobby", LoadSceneMode.Single);
                return;
            }

            Error(message);
        }
    }
}