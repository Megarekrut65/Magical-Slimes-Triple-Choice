using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Global.Button
{
    /// <summary>
    /// Class for button object that open scene by name
    /// </summary>
    public class OpenScene : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler {
        [SerializeField]
        private string sceneName;

        [SerializeField] 
        private bool openPrev = false;

        public void OnPointerUp(PointerEventData eventData) {
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            string prev = LocalStorage.GetValue("prevScene", "main");
            LocalStorage.SetValue("prevScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene(openPrev ? prev : sceneName, LoadSceneMode.Single);
        }
    }
}