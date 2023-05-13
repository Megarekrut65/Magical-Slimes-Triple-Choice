using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class ClipboardText : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Start()
        {
            text.text = Clipboard.Paste();
        }
    }
}