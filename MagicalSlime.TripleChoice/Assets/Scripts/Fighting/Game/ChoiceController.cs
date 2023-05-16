using UnityEngine;

namespace Fighting.Game
{
    public class ChoiceController : MonoBehaviour
    {
        public delegate void Choosing();

        public event Choosing Choice;
        
        public ChoiceType Block { get; protected set; }
        public ChoiceType Attack { get; protected set; }
        private int _clicks = 0;

        [SerializeField] private Animator animator;
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int BlockTrigger = Animator.StringToHash("Block");
        private static readonly int EndTrigger = Animator.StringToHash("End");

        public void StartChoice()
        {
            animator.SetTrigger(AttackTrigger);
        }

        private void Click()
        {
            _clicks++;
            if (_clicks < 2) return;
            Choice?.Invoke();
            _clicks = 0;
        }
        public void SelectAttack(int type)
        {
            Attack = (ChoiceType)(type%3);
            Click();
            animator.SetTrigger(BlockTrigger);
        }

        public void SelectBlock(int type)
        {
            Block = (ChoiceType)(type%3);
            Click();
            animator.SetTrigger(EndTrigger);
        }
    }
}