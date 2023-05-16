using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Messaging
{
    public class MessageTextController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Text text;
        
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void ShowMessage(string message, float time)
        {
            text.text = message;
            animator.SetTrigger(Show);
            StartCoroutine(Wait(time));
        }

        private IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time - 1f);
            animator.SetTrigger(Hide);
        }
    }
}