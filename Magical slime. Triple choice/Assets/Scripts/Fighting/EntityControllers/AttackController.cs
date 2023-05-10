using System.Collections.Generic;
using UnityEngine;

namespace Fighting.EntityControllers
{
    /**
     * Controls attacking to enemy using enemy blocking and health controllers
     */
    public class AttackController : MonoBehaviour
    {
        public static readonly Dictionary<ChoiceType, int> Converter = new Dictionary<ChoiceType, int>()
        {
            { ChoiceType.Top , Animator.StringToHash("AttackTop")},
            { ChoiceType.Center , Animator.StringToHash("AttackCenter")},
            { ChoiceType.Bottom , Animator.StringToHash("AttackBottom")}
        };
        
        [SerializeField] private BlockController enemyBlockController;
        [SerializeField] private HealthController enemyHealthController;
        [SerializeField] private ChoiceController enemyChoiceController;
        
        [SerializeField] private Animator animator;

        private ChoiceType _type = ChoiceType.None;
        private int _damageAmount = 10;
        
        public void Attack(ChoiceType type)
        {
            _type = type;
            animator.SetTrigger((int)type);
        }
        public void AttackEvent()
        {
            if (enemyChoiceController.Block == _type)
            {
                enemyBlockController.Block(_type);
                return;
            }
            enemyHealthController.TakeDamage(_damageAmount);
        }
    }
}