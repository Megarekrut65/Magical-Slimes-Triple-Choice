using System;
using System.Collections;
using Global;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private FirebaseLoader firebaseLoader;
        [SerializeField] private Slider slider;

        private void Start()
        {
            StartCoroutine(Loading());
        }

        private IEnumerator Loading()
        {
            for (float i = slider.maxValue; i >= slider.minValue; i-=0.1f)
            {
                slider.value = i;
                yield return new WaitForSeconds(0.1f);
            }

            while (!LocalizationManager.Instance.Ready || !firebaseLoader.Ready)
            {
                yield return new WaitForSeconds(0.05f);
            }

            bool skip = bool.Parse(LocalStorage.GetValue("skip-story", "false"));
            SceneManager.LoadScene(skip?"IncrementalMode":"Story", LoadSceneMode.Single);
        }
    }
}