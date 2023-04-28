using System;
using Firebase.Auth;
using UnityEngine;

namespace Main
{
    public class FirebaseManager : MonoBehaviour
    {
        private FirebaseAuth _auth;
        private FirebaseUser _user;
        
        public bool SignedIn { get; private set; }

        private void Start()
        {
            InitializeFirebase();
        }

        private void InitializeFirebase() {
            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        private void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            if (_auth.CurrentUser == _user) return;
            bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
            if (!signedIn && _user != null)
            {
                SignedIn = false;
            }
            _user = _auth.CurrentUser;
            SignedIn = true;
        }

        private void OnDestroy() {
            _auth.StateChanged -= AuthStateChanged;
            _auth = null;
        }
    }
}