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
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject sliderGameObject;
        public const int MaxHp = 100;
        private Slider _hpSlider;
        private int _currentHp;
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        public bool IsDied => _currentHp <= 0;

        private void Start()
        {
            _currentHp = DataSaver.LoadHp();
            if (IsDied)
            {
                Die();
            }
            
            _hpSlider = sliderGameObject.GetComponent<Slider>();
            _hpSlider.maxValue = MaxHp;
            _hpSlider.value = _currentHp;
        }

        public void TakeDamage(int value)
        {
            if(IsDied) return;
            _currentHp -= value;
            DataSaver.SaveHp(_currentHp);
            
            _hpSlider.value = _currentHp;
            if(IsDied) Die();
        }

        private void Die()
        {
            animator.speed = 1;
            sliderGameObject.SetActive(false);
            animator.SetBool(IsDie, true);
            
            StartCoroutine(AfterDie());
        }

        private IEnumerator AfterDie()
        {
            yield return new WaitForSeconds(3f);
            
            Action end = ()=>SceneManager.LoadScene("SlimeCreating", LoadSceneMode.Single);
            InfoBox.Instance.ShowInfo("Game Over", "Your Slime died. Create new one",end, end);
        }
    }
}