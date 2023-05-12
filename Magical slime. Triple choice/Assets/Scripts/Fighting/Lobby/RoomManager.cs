using Global.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Fighting.Lobby
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private Text errorText;

        protected void Click()
        {
            errorText.text = "";
        }
        protected void Error(string key)
        {
            errorText.text = LocalizationManager.GetWordByKey(key);
        }
    }
}