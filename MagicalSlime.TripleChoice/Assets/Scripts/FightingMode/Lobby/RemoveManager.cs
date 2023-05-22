using System;
using UnityEngine;

namespace FightingMode.Lobby
{
    public class RemoveManager : MonoBehaviour
    {
        private void Start()
        {
            RoomRemover.RemoveOld("private-rooms");
            RoomRemover.RemoveOld("global-rooms");
        }
    }
}