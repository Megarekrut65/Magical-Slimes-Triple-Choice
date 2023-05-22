using System.Collections;
using System.Collections.Generic;
using DataManagement;
using FightingMode.Game;
using Firebase.Database;
using Firebase.Extensions;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FightingMode.Lobby.PrivateLobby
{
    public class PrivateLobbyController : MonoBehaviour
    {
        [SerializeField] private Text error;
        
        [FormerlySerializedAs("countController")] [SerializeField] private Counter counter;
        
        [SerializeField] private EnemyController enemyController;
        
        private DatabaseReference _room;
        private bool _clientAlive, _firstCheck;

        private void Awake()
        {
            string code = FightingSaver.LoadCode();

            FirebaseDatabase db = FirebaseManager.Db;
            _room = db.RootReference.Child("private-rooms").Child(code);

            _room.ValueChanged += EnemyCome;
        }

        private void OnDestroy()
        {
            _room.ValueChanged -= EnemyCome;
        }

        private void Start()
        {
            if (FightingSaver.LoadMainType() == "host") return;
            _clientAlive = true;

            enemyController.Come(FightingSaver.LoadUserInfo("enemyInfo"));
            StartCoroutine(StartCount());
        }
        private void EnemyCome(object sender, ValueChangedEventArgs args)
        {
            if (!args.Snapshot.Exists)
            {
                error.text = LocalizationManager.GetWordByKey("host-leave");
                StartCoroutine(BackToLobby());
                return;
            }
            if(FightingSaver.LoadMainType() != "host") return;
            
            if (!args.Snapshot.Child("client").Exists)
            {
                if (!_firstCheck)
                {
                    _firstCheck = true;
                    return;
                }
                error.text = LocalizationManager.GetWordByKey("client-leave");
                _clientAlive = false;
                return;
            }
            UserInfo enemyInfo = UserInfo.FromDictionary(args.Snapshot.Child("client").Value as Dictionary<string, object>);
            if (enemyInfo == null)
            {
                error.text = LocalizationManager.GetWordByKey("room-error");
                return;
            }

            FightingSaver.SaveUserInfo("enemyInfo", enemyInfo);
            
            _clientAlive = true;
            enemyController.Come(enemyInfo);
            StartCoroutine(StartCount());
        }

        private IEnumerator StartCount()
        {
            yield return new WaitForSeconds(1f);
            int count = 3;
            counter.Invert(count+1);
            for (int i = 0; i < count; i++)
            {
                counter.ShowCount();
                yield return new WaitForSeconds(2f);
            }

            if(_clientAlive) SceneManager.LoadScene("Fighting", LoadSceneMode.Single);
        }

        public void Leave()
        {
            if (FightingSaver.LoadMainType() == "client")
            {
                _clientAlive = false;
                _room.Child("client").RemoveValueAsync().ContinueWithOnMainThread(_ =>
                {
                    StartCoroutine(BackToLobby());
                });
                return;
            }
            _room.RemoveValueAsync().ContinueWithOnMainThread(_ =>
            {
                StartCoroutine(BackToLobby());
            });
        }

        private IEnumerator BackToLobby()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }
    }
}