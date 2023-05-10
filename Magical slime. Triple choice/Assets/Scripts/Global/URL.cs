using UnityEngine;

namespace Global
{
    public class URL : MonoBehaviour
    {
        [SerializeField] private string url;
        
        public void Open()
        {
            Application.OpenURL(url);
        }
    }
}