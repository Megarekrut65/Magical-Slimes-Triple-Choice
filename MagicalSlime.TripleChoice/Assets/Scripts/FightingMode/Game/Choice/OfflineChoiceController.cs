using System;
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

        [SerializeField] private ChoiceTimer attackTimer;
        [SerializeField] private ChoiceTimer blockTimer;
        [SerializeField] private float waitTime;

        private ChoiceType _defaultChoice;
        
        private ChoiceDatabaseSender _databaseSender;

        private bool _attackClicked = true;
        private bool _blockClicked = true;

        private void Start()
        {
            _defaultChoice = FightingSaver.LoadDefaultChoice(FightingSaver.LoadMainType());
        }

        protected override void ChildAwake()
        {
            _databaseSender = new ChoiceDatabaseSender(FightingSaver.LoadMainType());
        }

        protected override void ChildDestroy()
        {
            
        }

        public override void StartChoice()
        {
            _attackClicked = false;
            _blockClicked = false;
            
            attackTimer.StartTimer(waitTime, ()=>SelectAttack((int)_defaultChoice));
            animator.SetTrigger(AttackTrigger);
        }
        public override void SelectAttack(int type)
        {
            attackTimer.StopTimer();
            
            if(_attackClicked) return;
            _attackClicked = true;

            base.SelectAttack(type);
            
            blockTimer.StartTimer(waitTime, ()=>SelectBlock((int)_defaultChoice));
            animator.SetTrigger(BlockTrigger);
            

            _databaseSender.SendAttack(type);
        }

        public override void SelectBlock(int type)
        {
            blockTimer.StopTimer();
            
            if(_blockClicked) return;
            _blockClicked = true;

            base.SelectBlock(type);
            animator.SetTrigger(EndTrigger);

            _databaseSender.SendBlock(type);
        }
    }
}