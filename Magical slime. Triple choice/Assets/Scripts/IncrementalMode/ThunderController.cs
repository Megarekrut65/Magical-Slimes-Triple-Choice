using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IncrementalMode
{
    public class ThunderController : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float speedValue;
        
        [SerializeField] private int minIndex;
        [SerializeField] private int maxIndex;
        [SerializeField] private Animator animator;

        private static readonly int Thunder = Animator.StringToHash("Thunder");


        private void Start()
        {
            StartCoroutine(DecreaseSpeed());
        }

        /// <summary>
        /// Calls random thunder animation
        /// </summary>
        public void Click()
        {
            IncreaseSpeed();
            int index = animator.GetInteger(Thunder);
            while (index == animator.GetInteger(Thunder))
            {
                index = Random.Range(minIndex, maxIndex);
            }
            animator.SetInteger(Thunder, index);
        }

        private void IncreaseSpeed()
        {
            if (!(animator.speed < maxSpeed)) return;
            float speed = animator.speed + speedValue;
            animator.speed = speed;
        }
        
        private IEnumerator DecreaseSpeed()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                if (!(animator.speed > 1)) continue;
                float speed = animator.speed - speedValue;
                animator.speed = speed;
            }
        }
    }
}