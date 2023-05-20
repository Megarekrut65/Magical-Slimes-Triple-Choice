using System;

namespace FightingMode.Game
{
    [Serializable]
    public class GameResult
    {
        public int deltaCups;
        public int deltaDiamonds;

        public GameResult(int deltaCups, int deltaDiamonds)
        {
            this.deltaCups = deltaCups;
            this.deltaDiamonds = deltaDiamonds;
        }
    }
}