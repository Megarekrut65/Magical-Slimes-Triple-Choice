using Fighting.Game;
using Global;
using Global.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace Fighting.Lobby.PrivateLobby
{
    public class CodeController : MonoBehaviour
    {
        [SerializeField] private Text codeText;

        private void Start()
        {
            string code = FightingSaver.LoadCode();
            if (code == "")
            {
                codeText.text = LocalizationManager.GetWordByKey("code-not-found");
                return;
            }
            codeText.text = code;
            Clipboard.Copy(code);
        }
    }
}