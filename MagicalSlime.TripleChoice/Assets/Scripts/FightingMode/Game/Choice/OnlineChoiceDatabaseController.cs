using System;
using Firebase.Database;
using UnityEngine;

namespace FightingMode.Game.Choice
{
    public class OnlineChoiceDatabaseController: ChoiceDatabaseController
    {
        public delegate void Answer(ChoiceType type);

        public event Answer Attack;
        public event Answer Block;
        
        public OnlineChoiceDatabaseController(string type) : base(type)
        {
            choice.ValueChanged += ChoiceHandler;
        }
        private void ChoiceHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists) return;
            choices.Clear();
            foreach (DataSnapshot data in args.Snapshot.Children)
            {
                choices.Add(JsonUtility.FromJson<ChoiceData>(data.Value as string));
            }
            
            InvokeNext();
        }

        public void InvokeNext()
        {
            if (count < choices.Count)
            {
                ChoiceData data = choices[count++];
                Attack?.Invoke((ChoiceType)data.attack);
                Block?.Invoke((ChoiceType)data.block);
            }
        }
    }
}