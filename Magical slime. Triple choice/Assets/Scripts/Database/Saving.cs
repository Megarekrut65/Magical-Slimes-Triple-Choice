using System;
using System.Collections;
using Firebase.Auth;
using Global;
using UnityEngine;

namespace DataBase
{
    public class Saving : MonoBehaviour
    {
        public static Saving Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            StartCoroutine(Save());
        }

        private IEnumerator Save()
        {
            DatabaseSaver saver = new DatabaseSaver();
            while (true)
            {
                yield return new WaitForSeconds(30f);
                if(LocalStorage.GetValue("needSave", "false") == "false") continue;
                
                FirebaseUser user = FirebaseAuth.DefaultInstance?.CurrentUser;
                if(user == null) continue;
                saver.SaveUserData(user.UserId, (_,_)=>Debug.Log("Saved..."));
            }
        }
    }
}