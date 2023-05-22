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
        private IEnumerator _attackEnumerator;
        private IEnumerator _blockEnumerator;
        
        private const float WaitTime = 60f;

        private ChoiceType _defaultChoice;

        private OnlineChoiceDatabaseController _databaseController;
        
        private void Start()
        {
            _defaultChoice = FightingSaver.LoadDefaultChoice(FightingSaver.LoadEnemyType());
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
            
            _attackEnumerator = AutoChoiceAttack();
            StartCoroutine(_attackEnumerator);
        }

        private void AttackChoice(ChoiceType type)
        {
            _attackChoice = true;
            SelectAttack((int)type);
            if(_attackEnumerator != null) StopCoroutine(_attackEnumerator);

            if (_blockChoice) return;
            _blockEnumerator = AutoChoiceBlock();
            StartCoroutine(_blockEnumerator);
        }
        private void BlockChoice(ChoiceType type)
        {
            _blockChoice = true;
            SelectBlock((int)type);
            if(_blockEnumerator != null) StopCoroutine(_blockEnumerator);
        }

        private IEnumerator AutoChoiceAttack()
        {
            yield return new WaitForSeconds(WaitTime/2);
            if (!_attackChoice) _databaseController.InvokeNext();
            yield return new WaitForSeconds(WaitTime/2);
            if (!_attackChoice) AttackChoice(_defaultChoice);
        }
        private IEnumerator AutoChoiceBlock()
        {
            yield return new WaitForSeconds(WaitTime/2);
            if (!_blockChoice) _databaseController.InvokeNext();
            yield return new WaitForSeconds(WaitTime/2);
            if (!_blockChoice) BlockChoice(_defaultChoice);
        }
    }
}