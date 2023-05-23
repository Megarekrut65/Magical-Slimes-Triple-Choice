using Global.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Lobby
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private Text errorText;
        [SerializeField] private GameObject loader;

        protected bool isClicked;
        
        protected void Click()
        {
            isClicked = true;
            
            errorText.text = "";
            loader.SetActive(true);
        }
        protected void Error(string key)
        {
            isClicked = false;
            
            errorText.text = LocalizationManager.GetWordByKey(key);
            loader.SetActive(false);
        }
    }
}