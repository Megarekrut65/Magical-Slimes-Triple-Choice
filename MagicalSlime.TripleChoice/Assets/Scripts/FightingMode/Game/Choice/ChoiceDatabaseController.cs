using System;
using System.Collections.Generic;
using Firebase.Database;

namespace FightingMode.Game.Choice
{
    public class ChoiceDatabaseController
    {
        protected readonly DatabaseReference choice;
        protected readonly List<ChoiceData> choices;

        protected int count = 0;

        public ChoiceDatabaseController(string type)
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference game = db.RootReference
                .Child(FightingSaver.LoadRoomType())
                .Child(FightingSaver.LoadCode())
                .Child("game");
            
            choice = game.Child(type).Child("choice");
            choices = new List<ChoiceData>();
        }
        
    }
}