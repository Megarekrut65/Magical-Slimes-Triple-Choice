using System.Collections;
using Global;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private FirebaseLoader firebaseLoader;

        private void Start()
        {
            StartCoroutine(Loading());
        }

        private IEnumerator Loading()
        {
            
            Screen.sleepTimeout = 0;//don't make screen dark during game
            
            yield return new WaitForSeconds(3.1f);
            
            while (!LocalizationManager.Instance.Ready || !firebaseLoader.Ready)
            {
                yield return new WaitForSeconds(0.05f);
            }

            Load();
        }

        private void Load()
        {
            if (Version.ToNumber(firebaseLoader.MinRequiredVersion) > Version.ToNumber(Version.Current))
            {
                SceneManager.LoadScene("Update", LoadSceneMode.Single);
                return;
            }
            bool skip = bool.Parse(LocalStorage.GetValue("skip-story", "false"));
            SceneManager.LoadScene(skip?"IncrementalMode":"Story", LoadSceneMode.Single);  
        }
    }
}