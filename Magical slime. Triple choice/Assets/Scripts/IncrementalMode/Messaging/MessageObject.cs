using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncrementalMode.Messaging
{
    public class MessageObject:MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Animator animator;
        private static readonly int Show = Animator.StringToHash("Show");

        public void SetMessage(string message)
        {
            text.text = message;
            text.color =
                new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        public void AnimationStart()
        {
            animator.SetBool(Show, true);
        }
        public void AnimationEnd()
        {
            animator.SetBool(Show, false);
        }
    }
}