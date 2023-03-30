using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.InfoBox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class Entity : MonoBehaviour
    {
        public delegate void EntityDied();

        public static event EntityDied OnEntityDied;
        public static event EntityDied OnEntityReLife;
        
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject sliderGameObject;
        public const int MaxHp = 100;
        private Slider _hpSlider;
        private int _currentHp;
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        public bool IsDied => _currentHp <= 0;
        
        public int AdditionalLife { get; set; }
        private bool imunity = false;

        private void Start()
        {
            _currentHp = DataSaver.LoadHp();
            if (IsDied)
            {
                StartCoroutine(Die());
            }
            
            _hpSlider = sliderGameObject.GetComponent<Slider>();
            _hpSlider.maxValue = MaxHp;
            _hpSlider.value = _currentHp;
        }

        public void Heal(int value)
        {
            if(IsDied) return;
            
            _currentHp += value;
            _currentHp = Math.Min(_currentHp, MaxHp);
            
            DataSaver.SaveHp(_currentHp);
            _hpSlider.value = _currentHp;
        }
        public void TakeDamage(int value)
        {
            if(IsDied || imunity) return;
            _currentHp -= value;
            DataSaver.SaveHp(_currentHp);
            
            _hpSlider.value = _currentHp;
            if (IsDied) StartCoroutine(Die());
        }

        private void ReLife()
        {
            imunity = true;
            DataSaver.SaveHp(MaxHp);
            StartCoroutine(NewLife());
            
            OnEntityReLife?.Invoke();
        }

        private IEnumerator NewLife()
        {
            float delta = _hpSlider.maxValue - _hpSlider.value;
            for (float i = 0f; i < delta; i++)
            {
                _hpSlider.value++;
                _currentHp++;
                yield return new WaitForSeconds(0.05f);
            }

            imunity = false;
        }
        private IEnumerator Die()
        {
            yield return new WaitForSeconds(0.5f);
            if (AdditionalLife > 0)
            {
                ReLife();
            }
            else
            {
                animator.speed = 1;
                sliderGameObject.SetActive(false);
                animator.SetBool(IsDie, true);
                OnEntityDied?.Invoke();
            }
        }
    }
}