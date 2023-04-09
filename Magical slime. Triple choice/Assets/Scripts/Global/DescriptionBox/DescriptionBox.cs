using Global.Localization;
using IncrementalMode;
using IncrementalMode.AutoFarming;
using UnityEngine;
using UnityEngine.UI;

namespace Global.DescriptionBox
{
    public class DescriptionBox : MonoBehaviour
    {
        [Header("Description Box")]
        [SerializeField] private Animator animator;
        
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text priceText;
        
        [SerializeField] private Text otherText;
        [SerializeField] private Text otherTitle;
        
        [SerializeField] private Image icon;
        
        private static readonly int Show = Animator.StringToHash("Show");
        
        private void Awake()
        {
            ShapeController.OnSpawning += HideBox;
        }

        private void OnDestroy()
        {
            ShapeController.OnSpawning -= HideBox;
        }

        public void ShowBox(DescriptionItem item)
        {
            titleText.text = LocalizationManager.TranslateWord(item.key);
            descriptionText.text = LocalizationManager.TranslateWord(item.key+"-description");
            priceText.text = new Energy(item.price).ToString();
            otherText.text = item.otherText;
            otherTitle.text = LocalizationManager.TranslateWord(item.otherTitleKey);
            icon.sprite = item.icon;

            animator.SetBool(Show, true);
        }

        public void HideBox()
        {
            if(animator == null || !animator.GetBool(Show)) return;
            
            animator.SetBool(Show, false);
        }
    }
}