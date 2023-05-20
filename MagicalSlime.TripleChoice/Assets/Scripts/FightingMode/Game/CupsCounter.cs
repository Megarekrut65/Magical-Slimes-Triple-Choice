using System;

namespace FightingMode.Game
{
    /// <summary>
    /// Counts new rating/cups of winner and loser. Uses formula from this page https://chess-math.org/scholastic-rating
    /// </summary>
    public class CupsCounter
    {
        private const int AddingConstant = 20;


        private readonly int _winnerCups;
        private readonly int _loserCups;

        public int NewWinnerCups
        {
            get
            {
                int delta = _winnerCups - _loserCups;
                return (int)Math.Floor(_winnerCups + AddingConstant + delta * 0.04);
            }
        }

        public int NewLoserCups
        {
            get
            {
                int delta = _loserCups - _winnerCups;
                return (int)Math.Floor(_loserCups - AddingConstant + delta * 0.04);
            }
        }

        public CupsCounter(int winnerCups, int loserCups)
        {
            _winnerCups = winnerCups;
            _loserCups = loserCups;
        }
    }
}