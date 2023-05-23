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
        [SerializeField] private Slider hpSlider;

        public const int MaxHp = 100;
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
            
            hpSlider.maxValue = MaxHp;
            hpSlider.value = _currentHp;
        }

        public void Heal(int value)
        {
            if(IsDied) return;
            hpAnimator.SetTrigger(HealTrigger);
            
            _currentHp += value;
            _currentHp = Math.Min(_currentHp, MaxHp);
            
            DataSaver.SaveHp(_currentHp);
            hpSlider.value = _currentHp;
        }
        public void TakeDamage(int value)
        {
            if(IsDied || _immunity) return;
            hpAnimator.SetTrigger(DamageTrigger);
            _currentHp -= value;
            DataSaver.SaveHp(_currentHp);
            
            hpSlider.value = _currentHp;
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
            float delta = hpSlider.maxValue - hpSlider.value;
            for (float i = 0f; i < delta; i++)
            {
                hpSlider.value++;
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
                slimeAnimator.speed = 1;
                slimeAnimator.SetBool(IsDie, true);
                OnEntityDied?.Invoke();
            }
        }

        public void Resurrect()
        {
            DataSaver.SaveShop("life", 2);
            ReLife();
            _died = false;
            slimeAnimator.SetBool(IsDie, false);
        }

        private void DieSound()
        {
            SoundManager.PlaySound(5);
        }
    }
}