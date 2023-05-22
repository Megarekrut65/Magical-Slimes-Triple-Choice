using Global;
using UnityEngine;

namespace FightingMode.Lobby
{
    public class LobbyManager : MonoBehaviour
    {
        private void Start()
        {
            LocalStorage.SetValue("needSave", "true");
        }
    }
}