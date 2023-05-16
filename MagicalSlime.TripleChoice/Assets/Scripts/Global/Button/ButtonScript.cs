using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Global.Button
{
    /// <summary>
    /// Class that make from GameObject button
    /// </summary>
    public class ButtonScript : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler {
        [SerializeField]
        private bool needSound = true;
        [SerializeField]
        private int soundIndex = 0;
        [SerializeField]
        private UnityEvent downEvent;
        [SerializeField]
        private UnityEvent upEvent;
        private ButtonEffect _buttonEffect;

        private void Start() {
            _buttonEffect = new ButtonEffect(transform, downEvent, upEvent, needSound, soundIndex);
        }
        public void OnPointerDown(PointerEventData eventData) {
            _buttonEffect?.Down();
        }
        public void OnPointerUp(PointerEventData eventData) {
            _buttonEffect?.Up();
        }
    }
}