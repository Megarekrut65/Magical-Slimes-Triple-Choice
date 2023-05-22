using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using UnityEngine;

namespace DataManagement
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager Instance { get; private set; }
        private FirebaseApp _app;
        private FirebaseDatabase _db;
        private FirebaseAuth _auth;
        private FirebaseFirestore _fs;

        public static FirebaseApp App => Instance? Instance._app : FirebaseApp.DefaultInstance;
        public static FirebaseDatabase Db => Instance? Instance._db : FirebaseDatabase.DefaultInstance;
        public static FirebaseAuth Auth => Instance? Instance._auth : FirebaseAuth.DefaultInstance;
        public static FirebaseFirestore Fs => Instance? Instance._fs : FirebaseFirestore.DefaultInstance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }

            LoadData();
            DontDestroyOnLoad(gameObject);
        }

        private void LoadData()
        {
            _app = FirebaseApp.DefaultInstance;
            _db = FirebaseDatabase.DefaultInstance;
            _auth = FirebaseAuth.DefaultInstance;
            _fs = FirebaseFirestore.DefaultInstance;
        }

    }
}