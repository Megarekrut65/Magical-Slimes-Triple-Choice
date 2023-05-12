using System;
using Global;
using UnityEngine;

namespace Fighting.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        private void Start()
        {
            LocalStorage.SetValue("needSave", "false");
        }
    }
}