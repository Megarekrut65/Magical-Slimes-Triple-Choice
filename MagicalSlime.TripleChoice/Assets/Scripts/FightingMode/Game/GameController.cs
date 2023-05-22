using Global;
using UnityEngine;

namespace FightingMode.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private RoundController roundController;
        [SerializeField] private ArrowController arrowController;
        
        private void Start()
        {
            LocalStorage.SetValue("needSave", "false");
            
            string firstType = FightingSaver.LoadFirst();
            string mainType = FightingSaver.LoadMainType();
            if (firstType == mainType)
            {
                arrowController.Left();
                return;
            }
            arrowController.Right();
        }

        private void Awake()
        {
            arrowController.EndEvent += roundController.StartGame;
        }

        private void OnDestroy()
        {
            arrowController.EndEvent -= roundController.StartGame;
        }
    }
}