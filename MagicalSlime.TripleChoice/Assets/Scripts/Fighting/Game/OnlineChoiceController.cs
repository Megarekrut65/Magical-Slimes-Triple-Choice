using System;
using Firebase.Database;
using UnityEngine;

namespace Fighting.Game
{
    public class OnlineChoiceController : ChoiceController
    {
        private DatabaseReference _gameChoice;
        private void Start()
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            _gameChoice = db.RootReference
                .Child(FightingSaver.LoadRoomType())
                .Child(FightingSaver.LoadCode())
                .Child("game")
                .Child(FightingSaver.LoadMainType());

            _gameChoice.ValueChanged += ChoiceHandler;
        }

        private void OnDestroy()
        {
            _gameChoice.ValueChanged -= ChoiceHandler;
        }

        private void ChoiceHandler(object sender, ValueChangedEventArgs args)
        {
            
        }
    }
}