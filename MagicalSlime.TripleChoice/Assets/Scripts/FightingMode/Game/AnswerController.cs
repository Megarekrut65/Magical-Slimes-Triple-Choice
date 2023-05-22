using System;
using System.Collections;
using Firebase.Database;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightingMode.Game
{
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
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
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

        private IEnumerator AutoAnswer()
        {
            yield return new WaitForSeconds(60f);
            if(!_isAnswer) AnswerEvent?.Invoke();
        }
    }
}