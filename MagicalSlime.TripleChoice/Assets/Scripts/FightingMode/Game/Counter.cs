using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Game
{
    /// <summary>
    /// Shows in GUI number in order
    /// </summary>
    public class Counter : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Text countText;
        
        private int _count = 0;
        private bool _invert = false;
        private static readonly int Show = Animator.StringToHash("Show");
        
        public void Invert(int count)
        {
            _count = count;
            _invert = true;
        }
        public void ShowCount()
        {
            if (_invert) _count--;
            else _count++;
            
            countText.text = _count.ToString();
            animator.SetTrigger(Show);
        }
        
    }
}