using UnityEngine;

namespace FightingMode.Game
{
    public class ArrowController : MonoBehaviour
    {
        public delegate void ArrowEnd();

        public event ArrowEnd EndEvent;
        
        
        [SerializeField] private Animator animator;
        private static readonly int LeftBool = Animator.StringToHash("Left");
        private static readonly int RightBool = Animator.StringToHash("Right");

        public void Left()
        {
            animator.SetBool(LeftBool, true);
        }
        public void Right()
        {
            animator.SetBool(RightBool, true);
        }

        public void End()
        {
            EndEvent?.Invoke();
        }
    }
}