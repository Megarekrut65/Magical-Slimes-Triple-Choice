using System;
using System.Collections;
using Account.SlimesList;
using Global;
using Global.InfoBox;
using Global.Localization;
using Global.Sound;
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
            
            MusicManager.Play();
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
            
            InfoBox.Instance.ShowInfo(LocalizationManager.GetWordByKey("game-over"), 
                LocalizationManager.GetWordByKey("slime-die"),end, end);
        }
    }
}