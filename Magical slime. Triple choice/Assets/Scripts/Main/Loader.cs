using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DataBase;
using Firebase.Extensions;
using Firebase.Firestore;

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
            
            Screen.sleepTimeout = 0;//don't make screen dark during game
            
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