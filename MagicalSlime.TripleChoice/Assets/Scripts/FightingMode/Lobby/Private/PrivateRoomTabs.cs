using UnityEngine;

namespace FightingMode.Lobby.Private
{
    public class PrivateRoomTabs : MonoBehaviour
    {
        [SerializeField] private GameObject createPlace;
        [SerializeField] private GameObject connectPlace;
        [SerializeField] private GameObject buttonsPlace;

        public void Create()
        {
            createPlace.SetActive(true);
            connectPlace.SetActive(false);
            buttonsPlace.SetActive(false);
        }

        public void Connect()
        {
            createPlace.SetActive(false);
            connectPlace.SetActive(true);
            buttonsPlace.SetActive(false);
        }

        public void Back()
        {
            createPlace.SetActive(false);
            connectPlace.SetActive(false);
            buttonsPlace.SetActive(true);
        }
    }
}