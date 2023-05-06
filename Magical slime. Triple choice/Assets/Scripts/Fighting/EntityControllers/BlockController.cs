using System;
using UnityEngine;

namespace Fighting.EntityControllers
{
    public static class BlockTypes
    {
        public static readonly int Top = Animator.StringToHash("BlockTop");
        public static readonly int Center = Animator.StringToHash("BlockCenter");
        public static readonly int Bottom = Animator.StringToHash("BlockBottom");
    }
    /**
     * Controls animations and events related to blocking damage
     */
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        public void Block(int type)
        {
            animator.SetTrigger(type);
        }
    }
}