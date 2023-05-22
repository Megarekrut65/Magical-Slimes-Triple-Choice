﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FightingMode.Lobby.Global
{
    public class GlobalRoomManager : RoomManager
    {
        [SerializeField] private Toggle toggle;
        
        public void CreateRoom()
        {
            Click();
            
            RoomController.AddGlobalRoom(UserInfoTaker.Take(), 100, Answer);
        }

        public void ConnectToRoom()
        {
            Click();
            
            RoomController.ConnectToGlobalRoom(UserInfoTaker.Take(), toggle.isOn, Answer);
        }

        private void Answer(bool success, string message)
        {
            if (success)
            {
                SceneManager.LoadScene("Fighting", LoadSceneMode.Single);
                return;
            }

            switch (message)
            {
                case "room-not-found":
                    CreateRoom();
                    return;
                case "host-not-alive":
                case "try-again":
                    ConnectToRoom();
                    return;
                default:
                    Error(message);
                    break;
            }
        }

        public void Back()
        {
            RoomController.Back(()=>SceneManager.LoadScene("IncrementalMode"));
        }
    }
}