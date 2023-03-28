using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.AutoFarming
{
    public class DescriptionBox : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text priceText;
        [SerializeField] private Text energyText;
        
        [SerializeField] private Image icon;
        [SerializeField] private StarsController stars;
        public void ShowBox(FarmInfo farmInfo)
        {
            titleText.text = farmInfo.title;
            icon.sprite = farmInfo.icon;
        }
    }
}