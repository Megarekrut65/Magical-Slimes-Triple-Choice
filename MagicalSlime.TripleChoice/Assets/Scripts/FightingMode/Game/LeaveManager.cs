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
        private DatabaseReference _room;
        
        private void Awake()
        {
            string code = FightingSaver.LoadCode();
            
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            _room = db.RootReference.Child(FightingSaver.LoadRoomType()).Child(code);
            _room.ValueChanged += RoomHandler;
        }

        private void OnDestroy()
        {
            _room.ValueChanged -= RoomHandler;
        }

        private void RoomHandler(object sender, ValueChangedEventArgs args)
        {
            
        }
        public void Leave()
        {
            _room.Child(FightingSaver.LoadMainType())
                .RemoveValueAsync()
                .ContinueWithOnMainThread(_ =>
                {
                    SceneManager.LoadScene("GameOver");
                });
        }
    }
}