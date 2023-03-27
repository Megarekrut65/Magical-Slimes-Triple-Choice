using System;
using Global;
using Unity.VisualScripting;
using UnityEngine;

namespace IncrementalMode.AutoFarming
{
    public class AutoFarmingBox : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void Click()
        {
            bool hide = animator.GetBool(Hide);
            animator.SetBool(Hide, !hide);
            LocalStorage.SetValue("AutoFarmingBoxHide", (!hide).ToString());
        }
        
        private void Start()
        {
            bool hide = Convert.ToBoolean(LocalStorage.GetValue("AutoFarmingBoxHide", "false"));
            animator.SetBool(Hide, hide);
        }
    }
}