using System.Collections;
using IncrementalMode.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


namespace IncrementalMode
{
    public class Clicking : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Animator animator;

        [SerializeField] private SpeedController speedController;
        [FormerlySerializedAs("moneyController")] [SerializeField] private EnergyController energyController;
        [SerializeField] private ShapeController shapeController;
        [SerializeField] private MessageController messaging;
        [SerializeField] private ThunderController thunderController;
        [SerializeField] private LevelController levelController;
        [SerializeField] private SoundClick soundClick;
        [SerializeField] private Entity mainCharacter;

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
            if(mainCharacter.IsDied) return;
            
            soundClick.Click();
            levelController.Click();
            
            ulong amount = energyController.Click(speedController.Percent, levelController.Level);
            messaging.Message(new Energy(amount).ToString());
            
            thunderController.Click();
            
            if(shapeController.Hit()) energyController.Click(amount * Mathf.Log(amount), levelController.Level);
            
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