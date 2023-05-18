using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Game
{
    /// <summary>
    /// Loads user data to game GUI
    /// </summary>
    public class UserInfoLoader : MonoBehaviour
    {
        [SerializeField] private string type;

        [SerializeField] private Text usernameText;
        [SerializeField] private Text slimeNameText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text cupsText;

        private void Start()
        {
            UserInfo info = FightingSaver.LoadUserInfo(type);
            usernameText.text = info.name;
            slimeNameText.text = info.slimeName;
            levelText.text = info.maxLevel.ToString();
            cupsText.text = info.cups.ToString();
        }
    }
}