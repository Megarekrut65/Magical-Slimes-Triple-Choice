using Firebase.Database;
using Global;
using UnityEngine;

namespace FightingMode.Game
{
    /// <summary>
    /// Show first player and starts game.
    /// </summary>
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
            
            FightingSaver.SaveGameOver(false);
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