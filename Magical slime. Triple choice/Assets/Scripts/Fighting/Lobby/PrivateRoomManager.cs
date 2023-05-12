using System;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Fighting.Lobby
{
    public class PrivateRoomManager : RoomManager
    {
        [SerializeField] private InputField maxHp;
        
        
        public void CreateRoom()
        {
            Click();
            
            RoomController.AddPrivateRoom(UserInfoTaker.Take(), 
                Convert.ToInt32(maxHp.text),
                (success, message) =>
                {
                    if (success)
                    {
                        SceneManager.LoadScene("PrivateLobby", LoadSceneMode.Single);
                        return;
                    }

                    Error(message);
                });
        }
    }
}