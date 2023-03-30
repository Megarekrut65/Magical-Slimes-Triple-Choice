using System;
using System.Collections;
using Global;
using Global.InfoBox;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private Text slimeText;
        private void Start()
        {
            if (LocalizationManager.Instance == null)
            {
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
                return;
            }
            string slimeName = DataSaver.LoadSlimeName();
            if (slimeName.Length == 0)
            {
                SceneManager.LoadScene("SlimeCreating", LoadSceneMode.Single);
            }

            slimeText.text = slimeName;
        }
        private void Awake()
        {
            Entity.OnEntityDied += Die;
        }

        private void OnDestroy()
        {
            Entity.OnEntityDied -= Die;
        }

        private void Die()
        {
            StartCoroutine(AfterDie());
        }
        private IEnumerator AfterDie()
        {
            yield return new WaitForSeconds(3f);
            
            Action end = ()=>SceneManager.LoadScene("SlimeCreating", LoadSceneMode.Single);
            
            InfoBox.Instance.ShowInfo(LocalizationManager.TranslateWord("game-over"), 
                LocalizationManager.TranslateWord("slime-die"),end, end);
        }
    }
}