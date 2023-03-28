using System;
using Global;
using UnityEngine;

namespace IncrementalMode.Shop
{
    public class ShopBox : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void Click()
        {
            bool value = animator.GetBool(Hide);
            animator.SetBool(Hide, !value);
            
            LocalStorage.SetValue("ShopBoxHide", (!value).ToString());
        }
        
        private void Start()
        {
            bool hide = Convert.ToBoolean(LocalStorage.GetValue("ShopBoxHide", "true"));
            animator.SetBool(Hide, hide);
        }
    }
}