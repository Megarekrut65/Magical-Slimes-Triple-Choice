using System;
using System.Collections;
using Account.SlimesList;
using Global;
using Global.Entity;
using Global.Sound;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace IncrementalMode
{
    /// <summary>
    /// Controls entity health. Damage taking, healing and dying.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        public delegate void EntityDied();

        public static event EntityDied OnEntityDied;
        public static event EntityDied OnEntityReLife;
        
        [SerializeField] private Animator slimeAnimator;
        [SerializeField] private Animator hpAnimator;
        [SerializeField] private GameObject sliderGameObject;

        public const int MaxHp = 100;
        private Slider _hpSlider;
        private int _currentHp;
        private static readonly int IsDie = Animator.StringToHash("IsDie");
        public bool IsDied => _currentHp <= 0;
        
        public int AdditionalLife { get; set; }
        private bool _immunity = false;
        private bool _died = false;
        private static readonly int HealTrigger = Animator.StringToHash("Heal");
        private static readonly int DamageTrigger = Animator.StringToHash("Damage");

        private void Start()
        {
            slimeAnimator.runtimeAnimatorController = EntityList.GetEntity(DataSaver.LoadSlimeType()).clickController;
            
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
            hpAnimator.SetTrigger(HealTrigger);
            
            _currentHp += value;
            _currentHp = Math.Min(_currentHp, MaxHp);
            
            DataSaver.SaveHp(_currentHp);
            _hpSlider.value = _currentHp;
        }
        public void TakeDamage(int value)
        {
            if(IsDied || _immunity) return;
            hpAnimator.SetTrigger(DamageTrigger);
            _currentHp -= value;
            DataSaver.SaveHp(_currentHp);
            
            _hpSlider.value = _currentHp;
            if (IsDied && !_died)
            {
                _died = true;
                StartCoroutine(Die());
            }
        }
        
        private void ReLife()
        {
            _immunity = true;
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
                hpAnimator.SetTrigger(HealTrigger);
                yield return new WaitForSeconds(0.05f);
            }

            _immunity = false;
        }
        private IEnumerator Die()
        {
            yield return new WaitForSeconds(0.5f);
            if (AdditionalLife > 0)
            {
                ReLife();
                _died = false;
            }
            else
            {
                SlimeDataSaver.SaveCurrentSlimeResult();
                slimeAnimator.speed = 1;
                sliderGameObject.SetActive(false);
                slimeAnimator.SetBool(IsDie, true);
                OnEntityDied?.Invoke();
            }
        }

        private void DieSound()
        {
            SoundManager.PlaySound(5);
        }
    }
}