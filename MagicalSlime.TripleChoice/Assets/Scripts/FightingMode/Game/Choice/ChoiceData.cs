using System;

namespace FightingMode.Game.Choice
{
    [Serializable]
    public class ChoiceData
    {
        public int attack;
        public int block;

        public ChoiceData(int attack, int block)
        {
            this.attack = attack;
            this.block = block;
        }

        public ChoiceData()
        {
        }
    }
}