using UnityEngine;

namespace Fighting.EntityControllers
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private AttackController attackController;
        [SerializeField] private ChoiceController choiceController;

        public void Attack()
        {
            int type = choiceController.Attack switch
            {
                ChoiceType.Top => AttackTypes.Top,
                ChoiceType.Center => AttackTypes.Center,
                _ => AttackTypes.Bottom
            };
            attackController.Attack(type);
        }
    }
}