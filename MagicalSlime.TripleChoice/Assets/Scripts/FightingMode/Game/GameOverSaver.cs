using System;

namespace FightingMode.Game
{
    /// <summary>
    /// Saves to local storage result of game.
    /// </summary>
    public class GameOverSaver
    {
        private readonly UserInfo _main;
        private readonly UserInfo _enemy;

        public GameOverSaver(UserInfo main, UserInfo enemy)
        {
            _main = main;
            _enemy = enemy;
        }

        public void Save(string winnerType, string mainType, string roomType)
        {
            UserInfo winner, loser;
            if (winnerType == mainType)
            {
                winner = _main;
                loser = _enemy;
            }
            else
            {
                winner = _enemy;
                loser = _main;
            }
            if(roomType == "global-rooms") SetNewParams(mainType,winnerType, winner, loser);
            else FightingSaver.SaveResult(new GameResult(0,0));
            
            SaveWinner(winner);
            SaveLoser(loser);
        }

        public void SaveDraw(string mainType, string roomType)
        {
            if(roomType == "global-rooms") SetNewParams(mainType, mainType, _main, _enemy);
            else FightingSaver.SaveResult(new GameResult(0,0));
            
            SaveWinner(_main);
            SaveLoser(_enemy);
        }

        private void SetNewParams(string mainType, string winnerType, UserInfo winner, UserInfo loser)
        {
            int winnerCups = winner.cups;
            int loserCups = loser.cups;
            
            CupsCounter counter = new CupsCounter(winnerCups, loserCups);
            winner.cups = counter.NewWinnerCups;
            loser.cups = counter.NewLoserCups;
            
            int winnerDiamonds = winner.maxLevel;
            int loserDiamonds = loser.maxLevel;
            
            CupsCounter diamondsCounter = new CupsCounter(winnerDiamonds, loserDiamonds);

            int newWinnerDiamonds = Math.Max(0, diamondsCounter.NewWinnerCups)/2;
            int newLoserDiamonds =  Math.Max(0, diamondsCounter.NewLoserCups)/8;

            GameResult result = mainType == winnerType 
                ? new GameResult(winner.cups - winnerCups, newWinnerDiamonds) 
                : new GameResult(loser.cups - loserCups, newLoserDiamonds);
            FightingSaver.SaveResult(result);
        }

        private void SaveWinner(UserInfo winner)
        {
            FightingSaver.SaveUserInfo("winner", winner);
        }

        private void SaveLoser(UserInfo loser)
        {
            FightingSaver.SaveUserInfo("loser", loser);
        }
    }
}