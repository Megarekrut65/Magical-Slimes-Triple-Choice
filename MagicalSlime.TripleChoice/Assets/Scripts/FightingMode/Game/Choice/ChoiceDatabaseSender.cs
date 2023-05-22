using System;
using UnityEngine;

namespace FightingMode.Game.Choice
{
    /// <summary>
    /// Sends player choice to database.
    /// </summary>
    public class ChoiceDatabaseSender: ChoiceDatabaseController
    {
        private readonly ChoiceData _data;
        
        public ChoiceDatabaseSender(string type) : base(type)
        {
            _data = new ChoiceData();
        }

        public void SendAttack(int type)
        {
            _data.attack = type;
        }
        public void SendBlock(int type)
        {
            _data.block = type;
            choices.Add(_data);
            
            choice.Child((choices.Count-1).ToString()).SetRawJsonValueAsync(JsonUtility.ToJson(_data));
        }
    }
}