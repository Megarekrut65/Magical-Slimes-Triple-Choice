using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;

namespace FightingMode.Game.Choice
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

        private OnlineChoiceDatabaseController _databaseController;
        
        private void Start()
        {
            _defaultChoice = FightingSaver.LoadDefaultChoice();
        }

        protected override void ChildAwake()
        {
            _databaseController = new OnlineChoiceDatabaseController(FightingSaver.LoadEnemyType());
            _databaseController.Attack += AttackChoice;
            _databaseController.Block += BlockChoice;
        }

        protected override void ChildDestroy()
        {
            _databaseController.Attack -= AttackChoice;
            _databaseController.Block -= BlockChoice;
        }

        public override void StartChoice()
        {
            _attackChoice = false;
            _blockChoice = false;
            StartCoroutine(AutoChoiceAttack());
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
            yield return new WaitForSeconds(WaitTime/2);
            _databaseController.InvokeNext();
            yield return new WaitForSeconds(WaitTime/2);
            if (!_attackChoice) AttackChoice(_defaultChoice);
        }
        private IEnumerator AutoChoiceBlock()
        {
            yield return new WaitForSeconds(WaitTime/2);
            _databaseController.InvokeNext();
            yield return new WaitForSeconds(WaitTime/2);
            if (!_blockChoice) BlockChoice(_defaultChoice);
        }
    }
}