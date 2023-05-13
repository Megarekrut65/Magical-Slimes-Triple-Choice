using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

namespace Fighting.Lobby.PrivateLobby
{
    public class PrivateLobbyController : MonoBehaviour
    {
        [SerializeField] private EnemyController enemyController;

        private DatabaseReference _client;

        private void Awake()
        {
            if (FightingSaver.LoadMainType() != "host") return;
            
            string code = FightingSaver.LoadCode();
            if(code == null) return;
            
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            _client = db.RootReference.Child("private-rooms").Child(code);
            _client.ValueChanged += EnemyCome;
        }

        private void OnDestroy()
        {
            if (FightingSaver.LoadMainType() != "host") return;
            
            _client.ValueChanged -= EnemyCome;
        }

        private void Start()
        {
            if (FightingSaver.LoadMainType() == "host") return;
            
            enemyController.Come(FightingSaver.LoadUserInfo("enemyInfo"));
        }
        private void EnemyCome(object sender, ValueChangedEventArgs args)
        {
            UserInfo enemyInfo = UserInfo.FromDictionary(args.Snapshot.Child("client").Value as Dictionary<string, object>);
            if (enemyInfo == null)
            {
                //TODO: Error handler
                return;
            }
            enemyController.Come(enemyInfo);
        }
    }
}