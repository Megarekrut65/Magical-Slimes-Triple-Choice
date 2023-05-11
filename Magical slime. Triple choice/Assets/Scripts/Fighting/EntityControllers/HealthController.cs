using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fighting.EntityControllers
{
    /**
     * Controls hp of entity (taking damage or healing)
     */
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Animator entityAnimator;
        [SerializeField] private Animator healthAnimator;
        
        [SerializeField] private Slider slider;
        [SerializeField] private Text hpText;

        private int _maxHp;
        private int _hp;
        
        private static readonly int HurtTrigger = Animator.StringToHash("Hurt");
        private static readonly int HealTrigger = Animator.StringToHash("Heal");
        private static readonly int DieAnimation = Animator.StringToHash("Die");

        public bool IsDied => _hp <= 0;

        private void Start()
        {
            _maxHp = 10;//FightingSaver.LoadMaxHp();
            
            slider.minValue = 0;
            slider.maxValue = _maxHp;
            slider.value = 0;
            slider.wholeNumbers = true;
            _hp = _maxHp;
            hpText.text = _hp.ToString();

            StartCoroutine(AddHealth());
        }

        private IEnumerator AddHealth()
        {
            for (int i = 0; i < _maxHp; i++)
            {
                slider.value = i + 1;
                yield return new WaitForSeconds(0.01f);
            }
        }
        public void TakeDamage(int damage)
        {
            healthAnimator.SetTrigger(HurtTrigger);
            
            _hp -= damage;
            _hp = Math.Max(_hp, 0);
            slider.value = _hp;
            hpText.text = _hp.ToString();
            
            entityAnimator.SetTrigger(HurtTrigger);
        }

        public void Heal(int amount)
        {
            healthAnimator.SetTrigger(HealTrigger);
            
            _hp += amount;
            _hp = Math.Min(_hp, _maxHp);
            slider.value = _hp;
            hpText.text = _hp.ToString();
        }

        public void Die()
        {
            entityAnimator.SetTrigger(DieAnimation);
        }
        
    }
}