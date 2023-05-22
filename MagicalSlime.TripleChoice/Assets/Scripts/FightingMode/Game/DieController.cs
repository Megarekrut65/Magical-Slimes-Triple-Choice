using System.Collections;
using FightingMode.Game.EntityControllers;
using Firebase.Database;
using Firebase.Extensions;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FightingMode.Game
{
    /// <summary>
    /// Saves winner and loser data after any slime is died.
    /// Also open game over scene.
    /// </summary>
    public class DieController : MonoBehaviour
    {
        [SerializeField] private HealthController mainHealthController;
        [SerializeField] private HealthController enemyHealthController;

        private int _losersCount = 0;
        private string _lastWinner = "";
        public bool IsGameOver()
        {
            return mainHealthController.IsDied || enemyHealthController.IsDied;
        }

        private void LoseEvent(HealthController healthController, string type)
        {
            if(!healthController.IsDied) return;
            healthController.Die();
            _lastWinner = type;
            _losersCount++;
        }
        public void GameOver()
        {
            string mainType = FightingSaver.LoadMainType();
            LoseEvent(mainHealthController, mainType);
            LoseEvent(enemyHealthController, FightingSaver.LoadEnemyType());

            GameOverSaver saver = new GameOverSaver(FightingSaver.LoadUserInfo("mainInfo"), 
                FightingSaver.LoadUserInfo("enemyInfo"));

            string roomType = FightingSaver.LoadRoomType();
            if (_losersCount == 1)
            {
                saver.Save(_lastWinner, mainType, roomType);
            }
            else
            {
                saver.SaveDraw(mainType, roomType);
            }
            
            CustomLogger.Log("GameOver!");
            
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference room = db.RootReference
                .Child(FightingSaver.LoadRoomType())
                .Child(FightingSaver.LoadCode());
            
            room.RemoveValueAsync().ContinueWithOnMainThread(_ =>
            {
                StartCoroutine(WaitForLoad());
            });
        }

        private IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
}