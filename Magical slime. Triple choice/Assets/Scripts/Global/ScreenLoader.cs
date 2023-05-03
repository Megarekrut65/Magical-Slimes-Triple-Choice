using UnityEngine;

namespace Global
{
    public class ScreenLoader : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        
        public void Show()
        {
            obj.SetActive(true);
        }

        public void Hide()
        {
            obj.SetActive(false);
        }
    }
}