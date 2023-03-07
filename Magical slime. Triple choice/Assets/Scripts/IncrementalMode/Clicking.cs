using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IncrementalMode
{
    public class Clicking : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Animator animator;

        [SerializeField] private SpeedController speedController;
        [SerializeField] private MoneyController moneyController;
        [SerializeField] private ShapeController shapeController;

        private int _clickingCount = 0;
        private bool _isRunning = false;
        private static readonly int IsClicking = Animator.StringToHash("IsClicking");

        private void Start()
        {
            StartCoroutine(NotClicking());
        }

        private IEnumerator NotClicking()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (_clickingCount == 0)
                {
                    animator.SetBool(IsClicking, false);
                    continue;
                }

                _clickingCount -= 10;
                if (_clickingCount < 0) _clickingCount = 0;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            moneyController.Click(speedController.Percent);
            if(shapeController.Hit()) moneyController.Click(100);
            _clickingCount++;
            speedController.Increase();
            animator.SetBool(IsClicking, true);
            if (_isRunning) return;
            animator.StartPlayback();
            _isRunning = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }
    }
}