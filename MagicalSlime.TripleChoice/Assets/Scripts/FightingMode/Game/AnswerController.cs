using System;
using System.Collections;
using DataManagement;
using Firebase.Database;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightingMode.Game
{
    /// <summary>
    /// Adds player answer of round is end to database and gets same answer from enemy.
    /// Only if answer is received game will be continue. But if there won't answer then it will gets automatically.
    /// </summary>
    public class AnswerController : MonoBehaviour
    {
        public delegate void Answer();

        public event Answer AnswerEvent;
        
        private DatabaseReference _enemyAnswer;
        private DatabaseReference _mainAnswer;

        private bool _isAnswer = false;
        private IEnumerator _answerEnumerator;
        
        private int _count;

        private void Awake()
        {
            FirebaseDatabase db = FirebaseManager.Db;
            DatabaseReference answer = db.RootReference
                .Child(FightingSaver.LoadRoomType())
                .Child(FightingSaver.LoadCode())
                .Child("game")
                .Child("answer");

            _enemyAnswer = answer.Child(FightingSaver.LoadEnemyType());
            _mainAnswer = answer.Child(FightingSaver.LoadMainType());

            _enemyAnswer.ValueChanged += AnswerHandler;
        }

        private void OnDestroy()
        {
            _enemyAnswer.ValueChanged -= AnswerHandler;
        }

        private void AnswerHandler(object sender, ValueChangedEventArgs args)
        {
            if (!args.Snapshot.Exists) return;
            _isAnswer = true;
            if(_answerEnumerator != null) StopCoroutine(_answerEnumerator);
            AnswerEvent?.Invoke();
        }

        /// <summary>
        /// Send answer of round is end to database.
        /// </summary>
        public void SendAnswer()
        {
            if (!_isAnswer)
            {
                _answerEnumerator = AutoAnswer();
                StartCoroutine(_answerEnumerator);
            }
            _isAnswer = false;
            _mainAnswer.SetValueAsync(_count++);
        }

        /// <summary>
        /// If answer won't received from enemy than it will got automatically. 
        /// </summary>
        /// <returns></returns>
        private IEnumerator AutoAnswer()
        {
            yield return new WaitForSeconds(60f);
            if(!_isAnswer) AnswerEvent?.Invoke();
        }
    }
}