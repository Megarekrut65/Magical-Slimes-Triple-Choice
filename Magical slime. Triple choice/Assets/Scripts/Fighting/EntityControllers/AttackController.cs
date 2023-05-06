using UnityEngine;

namespace Fighting.EntityControllers
{
    public static class AttackTypes
    {
        public static readonly int Top = Animator.StringToHash("AttackTop");
        public static readonly int Center = Animator.StringToHash("AttackCenter");
        public static readonly int Bottom = Animator.StringToHash("AttackBottom");
    }
    /**
     * Controls attacking to enemy using enemy blocking and health controllers
     */
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private BlockController enemyBlockController;
        [SerializeField] private HealthController enemyHealthController;
        [SerializeField] private ChoiceController enemyChoiceController;
        
        [SerializeField] private Animator animator;

        private int _type = -1;
        private int _damageAmount = 10;
        
        public void Attack(int type)
        {
            _type = type;
            animator.SetTrigger(type);
        }

        private void ChooseAttack(int attackType, int blockType, ChoiceType choiceType)
        {
            if (_type == attackType)//TOP
            {
                if (enemyChoiceController.Block == choiceType)
                {
                    enemyBlockController.Block(blockType);
                    return;
                }
                enemyHealthController.TakeDamage(_damageAmount);
            }
        }
        public void AttackEvent()
        {
            ChooseAttack(AttackTypes.Top, BlockTypes.Top, ChoiceType.Top);
            ChooseAttack(AttackTypes.Center, BlockTypes.Center, ChoiceType.Center);
            ChooseAttack(AttackTypes.Bottom, BlockTypes.Bottom, ChoiceType.Bottom);
        }
    }
}