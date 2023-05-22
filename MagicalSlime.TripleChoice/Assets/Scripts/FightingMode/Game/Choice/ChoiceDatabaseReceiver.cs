using System;
using Firebase.Database;
using UnityEngine;

namespace FightingMode.Game.Choice
{
    /// <summary>
    /// Receives enemy choice from database.
    /// </summary>
    public class ChoiceDatabaseReceiver: ChoiceDatabaseController
    {
        public delegate void Answer(ChoiceType type);

        public event Answer Attack;
        public event Answer Block;
        
        public ChoiceDatabaseReceiver(string type) : base(type)
        {
            choice.ValueChanged += ChoiceHandler;
        }
        private void ChoiceHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists) return;
            choices.Clear();
            foreach (DataSnapshot data in args.Snapshot.Children)
            {
                choices.Add(JsonUtility.FromJson<ChoiceData>(data.GetRawJsonValue()));
            }
            
            InvokeNext();
        }

        public void InvokeNext()
        {
            if (count >= choices.Count) return;
            ChoiceData data = choices[count];

            if(data == null) return;
            count++;
            Attack?.Invoke((ChoiceType)data.attack);
            Block?.Invoke((ChoiceType)data.block);
        }
    }
}