using UnityEngine;

namespace Global.Button
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Show = Animator.StringToHash("Show");

        public void Click()
        {
            bool isShow = animator.GetBool(Show);
            animator.SetBool(Show, !isShow);
        }
    }
}