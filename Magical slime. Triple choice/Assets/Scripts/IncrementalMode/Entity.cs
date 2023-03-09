using System;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject sliderGameObject;
        [SerializeField] private int maxHp;
        private Slider _hpSlider;
        private int _currentHp;
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        public bool IsDied => _currentHp <= 0;

        private void Start()
        {
            _currentHp = maxHp;
            _hpSlider = sliderGameObject.GetComponent<Slider>();
            _hpSlider.maxValue = maxHp;
            _hpSlider.value = maxHp;
        }

        public void TakeDamage(int value)
        {
            if(IsDied) return;
            _currentHp -= value;
            _hpSlider.value = _currentHp;
            if(IsDied) Die();
        }

        private void Die()
        {
            animator.speed = 1;
            sliderGameObject.SetActive(false);
            animator.SetBool(IsDie, true);
        }
    }
}