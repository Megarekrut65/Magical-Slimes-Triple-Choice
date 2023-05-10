using UnityEngine;

namespace Fighting.EntityControllers
{
    public enum ChoiceType
    {
        Top = 0,
        Center = 1,
        Bottom = 2,
        None = -1
    }
    public class ChoiceController : MonoBehaviour
    {
        public ChoiceType Attack => (ChoiceType)Random.Range(0,3);
        public ChoiceType Block => (ChoiceType)Random.Range(0,3);
    }
}