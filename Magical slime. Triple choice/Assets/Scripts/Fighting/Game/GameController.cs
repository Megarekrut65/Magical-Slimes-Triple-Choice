using System.Collections;
using Fighting.Game.EntityControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fighting.Game
{
    public class GameController : MonoBehaviour
    {

        [SerializeField] private ChoiceController main;
        [SerializeField] private ChoiceController enemy;

        [SerializeField] private AttackController mainAttackController;
        [SerializeField] private AttackController enemyAttackController;

        [SerializeField] private HealthController mainHealthController;
        [SerializeField] private HealthController enemyHealthController;

        [SerializeField] private CountController countController;
        
        private void Start()
        {
            StartCoroutine(ShowChoice());
        }

        private IEnumerator ShowChoice()
        {
            yield return new WaitForSeconds(0.5f);
            countController.ShowCount();
            
            yield return new WaitForSeconds(1.5f);
            main.StartChoice();
        }
        private int _count = 0;
        private void Awake()
        {
            main.Choice += Choice;
            enemy.Choice += Choice;

            mainAttackController.AttackFinish += FinishFirst;
            enemyAttackController.AttackFinish += FinishSecond;

        }

        private void OnDestroy()
        {
            main.Choice -= Choice;
            enemy.Choice -= Choice;
            
            mainAttackController.AttackFinish -= FinishFirst;
            enemyAttackController.AttackFinish -= FinishSecond;
        }

        private void Choice()
        {
            _count++;
            if (_count < 2)
            {
                enemy.StartChoice();
                return;
            }
            
            _count = 0;
            
            mainAttackController.Attack(main.Attack);
        }

        private void FinishFirst()
        {
            StartCoroutine(FinishFirstWait());
        }

        private IEnumerator FinishFirstWait()
        {
            yield return new WaitForSeconds(1.0f);
            
            enemyAttackController.Attack(enemy.Attack);
        }
        private void FinishSecond()
        {
            StartCoroutine(FinishSecondWait());
        }
        private IEnumerator FinishSecondWait()
        {
            yield return new WaitForSeconds(1.5f);
            
            if (!mainHealthController.IsDied && !enemyHealthController.IsDied)
            {
                StartCoroutine(ShowChoice());
            }
            else
            {
                if(mainHealthController.IsDied) mainHealthController.Die();
                if(enemyHealthController.IsDied) enemyHealthController.Die();
                Debug.Log("GameOver!");
                yield return new WaitForSeconds(4f);
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            }
        }
    }
}