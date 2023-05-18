using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Lobby
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