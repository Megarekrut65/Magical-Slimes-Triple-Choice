﻿using Firebase.Database;
using UnityEngine;

namespace FightingMode.Game
{
    /// <summary>
    /// Controls selecting attack and block in right way. After selecting invokes Choosing event.
    /// Base class for offline and online controllers.
    /// </summary>
    public abstract class ChoiceController : MonoBehaviour
    {
        public delegate void Choosing();
        public event Choosing Choice;
        public ChoiceType Block { get; protected set; }
        public ChoiceType Attack { get; protected set; }
        private int _clicks = 0;
        
        protected DatabaseReference attackRef;
        protected DatabaseReference blockRef;
        
        private void Awake()
        {
            FirebaseDatabase db = FirebaseDatabase.DefaultInstance;
            DatabaseReference gameChoice = db.RootReference
                .Child(FightingSaver.LoadRoomType())
                .Child(FightingSaver.LoadCode())
                .Child("game")
                .Child(FightingSaver.LoadMainType());

            attackRef = gameChoice.Child("attack");
            blockRef = gameChoice.Child("block");
            
            ChildAwake();
        }

        private void OnDestroy()
        {
            ChildDestroy();
        }

        protected virtual void ChildAwake()
        {
            
        }

        protected virtual void ChildDestroy()
        {
            
        }

        public abstract void StartChoice();

        private void Click()
        {
            _clicks++;
            if (_clicks < 2) return;
            Choice?.Invoke();
            _clicks = 0;
        }
        public virtual void SelectAttack(int type)
        {
            Attack = (ChoiceType)(type%3);
            Click();
        }

        public virtual void SelectBlock(int type)
        {
            Block = (ChoiceType)(type%3);
            Click();
        }
    }
}