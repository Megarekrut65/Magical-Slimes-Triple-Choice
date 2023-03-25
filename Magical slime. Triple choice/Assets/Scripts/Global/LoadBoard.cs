using UnityEngine;

namespace Global
{
    /// <summary>
    /// Control class for black board with circle animation. Use for loading
    /// </summary>
    public class LoadBoard {
        /// <summary>Prefab to create in scene</summary>
        private readonly GameObject _board;

        /// <summary>
        /// Instantiates black board in canvas
        /// </summary>
        /// <param name="blackBoard">GameObject with black background and circle animation</param>
        /// <param name="canvas">Canvas in scene</param>
        public LoadBoard(GameObject blackBoard, GameObject canvas)
        {
            if (blackBoard == null || canvas == null) return;
            
            _board = Object.Instantiate(blackBoard, new Vector3(0, 0, 0), Quaternion.identity);
            _board.GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
            _board.transform.SetParent(canvas.transform, false);
            SetActive(false);
        }
        /// <summary>
        /// Show and hides board
        /// </summary>
        /// <param name="value">True to show board and false to hide</param>
        public void SetActive(bool value) {
            _board.SetActive(value);
        }
    }
}