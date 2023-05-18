using System.Collections.Generic;
using UnityEngine;

namespace FightingMode.Game.EntityControllers
{
    /**
     * Controls animations and events related to blocking damage
     */
    public class BlockController : MonoBehaviour
    {
        public static readonly Dictionary<ChoiceType, int> Converter = new Dictionary<ChoiceType, int>()
        {
            { ChoiceType.Top , Animator.StringToHash("BlockTop")},
            { ChoiceType.Center , Animator.StringToHash("BlockCenter")},
            { ChoiceType.Bottom , Animator.StringToHash("BlockBottom")}
        };
    
        [SerializeField] private Animator animator;
        
        public void Block(ChoiceType type)
        {
            animator.SetTrigger(Converter[type]);
        }
    }
}