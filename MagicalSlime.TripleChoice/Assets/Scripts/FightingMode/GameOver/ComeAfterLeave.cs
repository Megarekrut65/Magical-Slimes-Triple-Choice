using FightingMode.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FightingMode.GameOver
{
    public class ComeAfterLeave : MonoBehaviour
    {
        private void Start()
        {
            if (FightingSaver.LoadGameOver()) return;
            SetResult();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        private void SetResult()
        {
            GameOverSaver saver = new GameOverSaver(FightingSaver.LoadUserInfo("mainInfo"), 
                FightingSaver.LoadUserInfo("enemyInfo"));

            string roomType = FightingSaver.LoadRoomType(), mainType= FightingSaver.LoadMainType();
            saver.Save(FightingSaver.LoadEnemyType(), mainType, roomType);
        }
    }
}