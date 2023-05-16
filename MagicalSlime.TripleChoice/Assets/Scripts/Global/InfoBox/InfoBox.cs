using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Global.InfoBox
{
    public class InfoBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static InfoBox Instance { get; private set; }
        
        [SerializeField] private Animator animator;
        
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        
        [SerializeField] private InfoBoxButton button;
        
        private Action _cancelAction = ()=>{};
        private static readonly int Show = Animator.StringToHash("Show");

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            
            titleText.text = "";
            descriptionText.text = "";
            _cancelAction = ()=>{};
            button.Click = () => { };
            
        }
        public void ShowInfo(string title, string description, Action ok, Action cancel)
        {
            titleText.text = title;
            descriptionText.text = description;
            _cancelAction = cancel;
            
            button.Click = ()=>
            {
                animator.SetBool(Show, false);
                ok();
            };
            
            animator.SetBool(Show, true);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _cancelAction();
            animator.SetBool(Show, false);
        }
    }
}