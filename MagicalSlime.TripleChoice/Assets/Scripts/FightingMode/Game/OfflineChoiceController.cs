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