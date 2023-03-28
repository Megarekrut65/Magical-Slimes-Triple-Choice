using System;
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
        
        private static readonly int Show = Animator.StringToHash("Show");

        private void Start()
        {
            ShapeController.OnSpawning += HideBox;
        }

        public void ShowBox(Farm farm)
        {
            titleText.text = farm.Info.title;
            descriptionText.text = farm.Info.description;
            priceText.text = farm.Price.ToString();
            energyText.text = farm.Amount.ToString();
            icon.sprite = farm.Info.icon;
            
            stars.SetStars(farm.Info.level);
            
            animator.SetBool(Show, true);
        }

        public void HideBox()
        {
            if(animator == null || !animator.GetBool(Show)) return;
            
            animator.SetBool(Show, false);
        }
    }
}