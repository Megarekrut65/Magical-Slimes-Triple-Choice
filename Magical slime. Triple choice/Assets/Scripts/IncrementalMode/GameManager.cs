using System;
using Global;
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
            string slimeName = DataSaver.LoadSlimeName();
            if (slimeName.Length == 0)
            {
                SceneManager.LoadScene("SlimeCreating", LoadSceneMode.Single);
            }

            slimeText.text = slimeName;
        }
    }
}