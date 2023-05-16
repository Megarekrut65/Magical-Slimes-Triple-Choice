using Fighting.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Fighting.Lobby
{
    public class CupsLoader : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Start()
        {
            text.text = FightingSaver.LoadCups().ToString();
        }
    }
}