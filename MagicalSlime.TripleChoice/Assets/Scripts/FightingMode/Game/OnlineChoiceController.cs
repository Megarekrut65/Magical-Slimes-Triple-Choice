using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;

namespace FightingMode.Game
{
    /// <summary>
    /// Gets enemy selecting from database. If there aren't any selecting then automatically selects default types. 
    /// </summary>
    public class OnlineChoiceController : ChoiceController
    {

        private bool _attackChoice = false;
        private bool _blockChoice = false;
        private const float WaitTime = 60f;

        private ChoiceType _defaultChoice;
        
        private void Start()
        {
            _defaultChoice = FightingSaver.LoadDefaultChoice();
        }

        protected override void ChildAwake()
        {
            attackRef = gameChoice.Child(FightingSaver.LoadEnemyType()).Child("attack");
            blockRef = gameChoice.Child(FightingSaver.LoadEnemyType()).Child("block");
            
            attackRef.ValueChanged += AttackChoiceHandler;
            blockRef.ValueChanged += BlockChoiceHandler;
        }

        protected override void ChildDestroy()
        {
            attackRef.ValueChanged -= AttackChoiceHandler;
            blockRef.ValueChanged -= BlockChoiceHandler;
        }

        public override void StartChoice()
        {
            _attackChoice = false;
            _blockChoice = false;
            StartCoroutine(AutoChoiceAttack());
        }

        private void AttackChoiceHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists) return;
            int choice = Convert.ToInt32(args.Snapshot.Value);
            if(ChoiceTypeCorrect.IsCorrect(choice)) 
                AttackChoice((ChoiceType)choice);
        }
        private void BlockChoiceHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists) return;
            int choice = Convert.ToInt32(args.Snapshot.Value);
            if(ChoiceTypeCorrect.IsCorrect(choice)) 
                BlockChoice((ChoiceType)choice);
        }

        private void AttackChoice(ChoiceType type)
        {
            _attackChoice = true;
            SelectAttack((int)type);
            StopCoroutine(AutoChoiceAttack());
            if (!_blockChoice) StartCoroutine(AutoChoiceBlock());
        }
        private void BlockChoice(ChoiceType type)
        {
            _blockChoice = true;
            SelectBlock((int)type);
            StopCoroutine(AutoChoiceBlock());
        }

        private IEnumerator AutoChoiceAttack()
        {
            yield return new WaitForSeconds(WaitTime);
            if (!_attackChoice) AttackChoice(_defaultChoice);
        }
        private IEnumerator AutoChoiceBlock()
        {
            yield return new WaitForSeconds(WaitTime);
            if (!_blockChoice) BlockChoice(_defaultChoice);
        }
    }
}