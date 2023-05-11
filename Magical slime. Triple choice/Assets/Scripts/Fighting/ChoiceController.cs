using System;
using UnityEngine;

namespace Fighting
{
    public class ChoiceController : MonoBehaviour
    {
        public delegate void Choosing();

        public event Choosing Choice;
        
        public ChoiceType Block { get; protected set; }
        public ChoiceType Attack { get; protected set; }
        private int _clicks = 0;

        [SerializeField] private GameObject attackBorder;
        [SerializeField] private GameObject blockBorder;

        private void Start()
        {
            attackBorder.SetActive(false);
            blockBorder.SetActive(false);
        }

        public void StartChoice()
        {
            attackBorder.SetActive(true);
        }

        private void Click()
        {
            _clicks++;
            if (_clicks < 2) return;
            Choice?.Invoke();
            _clicks = 0;
        }
        public void SelectAttack(int type)
        {
            Attack = (ChoiceType)(type%3);
            Click();
            attackBorder.SetActive(false);
            blockBorder.SetActive(true);
        }

        public void SelectBlock(int type)
        {
            Block = (ChoiceType)(type%3);
            Click();
            blockBorder.SetActive(false);
        }
    }
}