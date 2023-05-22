using UnityEngine;

namespace FightingMode.Game.Choice
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

        private ChoiceDatabaseSender _databaseSender;

        protected override void ChildAwake()
        {
            _databaseSender = new ChoiceDatabaseSender(FightingSaver.LoadMainType());
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

            _databaseSender.SendAttack(type);
        }

        public override void SelectBlock(int type)
        {
            base.SelectBlock(type);
            animator.SetTrigger(EndTrigger);

            _databaseSender.SendBlock(type);
        }
    }
}