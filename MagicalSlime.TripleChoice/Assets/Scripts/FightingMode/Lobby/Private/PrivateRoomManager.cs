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
            Click();
            
            RoomController.AddPrivateRoom(UserInfoTaker.Take(), Convert.ToInt32(maxHp.text),Answer);
        }

        public void ConnectToRoom()
        {
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