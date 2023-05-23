using System;
using System.Collections.Generic;
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
            count = -1;
        }
        private void ChoiceHandler(object sender, ValueChangedEventArgs args)
        {
            if(!args.Snapshot.Exists) return;
            choices.Clear();
            choices.AddRange(new ChoiceData[args.Snapshot.ChildrenCount]);
            foreach (DataSnapshot data in args.Snapshot.Children)
            {
                choices[Convert.ToInt32(data.Key)] = JsonUtility.FromJson<ChoiceData>(data.GetRawJsonValue());
            }

            InvokeNext();
        }

        public void InvokeNext()
        {
            if (count < 0 || count >= choices.Count) return;
            ChoiceData data = choices[count];

            if(data == null) return;
            Attack?.Invoke((ChoiceType)data.attack);
            Block?.Invoke((ChoiceType)data.block);
        }

        public void NextRound()
        {
            count++;
        }
    }
}