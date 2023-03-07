using System;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private GameObject sliderFill;
        [SerializeField] private int maxHp;
        private int _currentHp;
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        public bool IsDied => _currentHp <= 0;

        private void Start()
        {
            _currentHp = maxHp;
            hpSlider.maxValue = maxHp;
        }

        public void TakeDamage(int value)
        {
            if(IsDied) return;
            _currentHp -= value;
            hpSlider.value = _currentHp;
            if(IsDied) Die();
        }

        private void Die()
        {
            sliderFill.SetActive(false);
            animator.SetBool(IsDie, true);
        }
    }
}