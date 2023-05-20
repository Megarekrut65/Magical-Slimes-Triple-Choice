using System;
using System.Collections;
using FightingMode.Game.Choice;
using FightingMode.Game.EntityControllers;
using UnityEngine;

namespace FightingMode.Game
{
    /// <summary>
    /// Controls the sequence of events in the game. Like attacking and blocking, new round or game over.
    /// </summary>
    public class RoundController : MonoBehaviour
    {
        
        [SerializeField] private ChoiceController main;
        [SerializeField] private ChoiceController enemy;

        [SerializeField] private AttackController mainAttackController;
        [SerializeField] private AttackController enemyAttackController;

        [SerializeField] private DieController dieController;

        [SerializeField] private AnswerController answerController;

        [SerializeField] private Counter counter;
        
        private int _count = 0;

        private string _firstType;
        private string _mainType;
        
        private AttackController.AttackAnimationFinish _mainFinish;
        private AttackController.AttackAnimationFinish _enemyFinish;
        
        private int _ready;

        public void StartGame()
        {
            StartCoroutine(ShowChoice());
        }

        public void NextRound()
        {
            _ready++;
            if (_ready != 2) return;
            _ready = 0;
            StartCoroutine(ShowChoice());
        }
        private IEnumerator ShowChoice()
        {
            yield return new WaitForSeconds(0.5f);
            counter.ShowCount();
            
            yield return new WaitForSeconds(1.5f);
            main.StartChoice();
            enemy.StartChoice();
        }
        
        private void Awake()
        {
            answerController.AnswerEvent += NextRound;
            
            main.Choice += Choice;
            enemy.Choice += Choice;
            
            _firstType = FightingSaver.LoadFirst();
            _mainType = FightingSaver.LoadMainType();
            if (_firstType == _mainType)
            {
                _mainFinish = FinishFirst;
                _enemyFinish = FinishSecond;
            }
            else
            {
                _mainFinish = FinishSecond;
                _enemyFinish = FinishFirst;
            }            

            mainAttackController.AttackFinish += _mainFinish;
            enemyAttackController.AttackFinish += _enemyFinish;
        }

        private void OnDestroy()
        {
            answerController.AnswerEvent -= NextRound;
            
            main.Choice -= Choice;
            enemy.Choice -= Choice;
            
            mainAttackController.AttackFinish -= _mainFinish;
            enemyAttackController.AttackFinish -= _enemyFinish;
        }
        private void Choice()
        {
            _count++;
            if (_count < 2) return;
            
            _count = 0;

            if(_firstType == _mainType) mainAttackController.Attack(main.Attack);
            else enemyAttackController.Attack(enemy.Attack);
        }

        private void FinishFirst()
        {
            StartCoroutine(FinishFirstWait());
        }

        private IEnumerator FinishFirstWait()
        {
            yield return new WaitForSeconds(1.0f);
            
            if(_firstType == _mainType) enemyAttackController.Attack(enemy.Attack);
            else mainAttackController.Attack(main.Attack);
        }
        private void FinishSecond()
        {
            StartCoroutine(FinishSecondWait());
        }
        private IEnumerator FinishSecondWait()
        {
            yield return new WaitForSeconds(1.5f);
            
            if (!dieController.IsGameOver())
            {
                NextRound();
                answerController.SendAnswer();
            }
            else
            {
                dieController.GameOver();
            }
        }
    }
}