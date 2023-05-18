using UnityEngine;

namespace Global.Localization
{
    /// <summary>
    /// Shows in GUI loading during language changing.
    /// </summary>
    public class LanguageLoader : MonoBehaviour {
        [SerializeField]
        private GameObject blackBoard;
        [SerializeField]
        private GameObject mainCanvas;
        private LoadBoard _loadBoard;

        private void Start() {
            _loadBoard = new LoadBoard(blackBoard, mainCanvas);
        }
        public void SetActive(bool value) {
            _loadBoard.SetActive(value);
        }
    }
}