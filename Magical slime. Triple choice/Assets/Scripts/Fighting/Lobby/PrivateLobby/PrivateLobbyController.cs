using System.Collections;
using System.Collections.Generic;
using Fighting.Game;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fighting.Lobby.PrivateLobby
{
    public class PrivateLobbyController : MonoBehaviour
    {
        [SerializeField] private CountController countController;
        
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
            StartCoroutine(StartCount());
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
            StartCoroutine(StartCount());
        }

        private IEnumerator StartCount()
        {
            yield return new WaitForSeconds(1f);
            int count = 3;
            countController.Invert(count+1);
            for (int i = 0; i < count; i++)
            {
                countController.ShowCount();
                yield return new WaitForSeconds(2f);
            }

            SceneManager.LoadScene("Fighting", LoadSceneMode.Single);
        }
    }
}