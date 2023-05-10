using UnityEngine;

namespace Fighting.EntityControllers
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private AttackController attackController;
        [SerializeField] private ChoiceController choiceController;

        public void Attack()
        {
            attackController.Attack(choiceController.Attack);
        }
    }
}