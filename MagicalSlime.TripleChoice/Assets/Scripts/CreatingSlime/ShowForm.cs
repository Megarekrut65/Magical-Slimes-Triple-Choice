using System;
using UnityEngine;

namespace CreatingSlime
{
    public class ShowForm : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private static readonly int Show = Animator.StringToHash("Show");

        private void Start()
        {
            animator.SetBool(Show, true);
        }
    }
}