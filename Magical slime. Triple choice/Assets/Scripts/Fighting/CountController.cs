using UnityEngine;
using UnityEngine.UI;

namespace Fighting
{
    public class CountController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Text countText;
        
        private int _count = 0;
        private static readonly int Show = Animator.StringToHash("Show");

        public void ShowCount()
        {
            _count++;
            countText.text = _count.ToString();
            animator.SetTrigger(Show);
        }
    }
}