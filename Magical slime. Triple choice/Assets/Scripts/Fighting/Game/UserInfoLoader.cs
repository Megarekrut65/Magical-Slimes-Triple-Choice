using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fighting.Game
{
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