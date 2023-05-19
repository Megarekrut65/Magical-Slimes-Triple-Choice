using UnityEngine;

namespace FightingMode.Game
{
    /// <summary>
    /// Manages selecting attack and block using clicking on buttons and puts this selecting to database.
    /// </summary>
    public class OfflineChoiceController : ChoiceController
    {
        [SerializeField] private Animator animator;
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int BlockTrigger = Animator.StringToHash("Block");
        private static readonly int EndTrigger = Animator.StringToHash("End");

        protected override void ChildAwake()
        {
            attackRef = gameChoice.Child(FightingSaver.LoadMainType()).Child("attack");
            blockRef = gameChoice.Child(FightingSaver.LoadMainType()).Child("block");
        }

        protected override void ChildDestroy()
        {
            
        }

        public override void StartChoice()
        {
            animator.SetTrigger(AttackTrigger);
        }
        public override void SelectAttack(int type)
        {
            base.SelectAttack(type);
            animator.SetTrigger(BlockTrigger);

            attackRef.SetValueAsync(type);
        }

        public override void SelectBlock(int type)
        {
            base.SelectBlock(type);
            animator.SetTrigger(EndTrigger);

            blockRef.SetValueAsync(type);
        }
    }
}