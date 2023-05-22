using System.Collections;
using Global.Hats;
using UnityEngine;

namespace Global
{
    /// <summary>
    /// Gets hat data by key
    /// </summary>
    public class CoroutineStarter : MonoBehaviour
    {
        public static CoroutineStarter Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public static void StartOne(IEnumerator coroutine)
        {
            Instance?.StartCoroutine(coroutine);
        }
        public static void StopOne(IEnumerator coroutine)
        {
            if(coroutine == null) return;
            
            Instance?.StopCoroutine(coroutine);
        }
    }
}