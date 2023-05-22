using System.Collections;
using System.Threading.Tasks;
using DataManagement;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FightingMode.Game
{
    /// <summary>
    /// Removes player data from document in database after player leave game.
    /// For other player take this changing and inform about enemy left.
    /// </summary>
    public class LeaveManager : MonoBehaviour
    {
        [SerializeField] private DieController dieController;
        
        private DatabaseReference _room;
        private bool _leaved;
        
        private void Awake()
        {
            string code = FightingSaver.LoadCode();

            FirebaseDatabase db = FirebaseManager.Db;
            _room = db.RootReference.Child(FightingSaver.LoadRoomType()).Child(code);
            _room.ValueChanged += RoomHandler;
        }

        private void OnDestroy()
        {
            _room.ValueChanged -= RoomHandler;
        }

        private void SetResult(string winnerType)
        {
            GameOverSaver saver = new GameOverSaver(FightingSaver.LoadUserInfo("mainInfo"), 
                FightingSaver.LoadUserInfo("enemyInfo"));

            string roomType = FightingSaver.LoadRoomType(), mainType= FightingSaver.LoadMainType();
            saver.Save(winnerType, mainType, roomType);
        }
        private void RoomHandler(object sender, ValueChangedEventArgs args)
        {
            if (dieController.IsGameOver() || _leaved || args.Snapshot.Exists) return;
            SetResult(FightingSaver.LoadMainType());
            
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        public void Leave()
        {
            _leaved = true;
            _room.RemoveValueAsync().ContinueWithOnMainThread(LeaveTask);

            StartCoroutine(AutoLeave());
        }

        private void LeaveTask(Task _)
        {
            StopCoroutine(AutoLeave());
            SetResult(FightingSaver.LoadEnemyType());

            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

        private IEnumerator AutoLeave()
        {
            yield return new WaitForSeconds(5f);

            LeaveTask(null);
        }
    }
}