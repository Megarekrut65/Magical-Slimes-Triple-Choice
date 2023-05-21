using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Lobby.Rating
{
    public class RatingItem : MonoBehaviour
    {
        [SerializeField] private Text usernameText;
        [SerializeField] private Text cupsText;
        [SerializeField] private Text maxLevelText;
        [SerializeField] private Text maxEnergyText;

        [SerializeField] private Image background;

        public void SetRating(Rating rating, Color color)
        {
            usernameText.text = rating.username;
            cupsText.text = rating.cups.ToString();
            maxLevelText.text = rating.maxLevel.ToString();
            maxEnergyText.text = rating.maxEnergy;

            background.color = color;
        }
    }
}