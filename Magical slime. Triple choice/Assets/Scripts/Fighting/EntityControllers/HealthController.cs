using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fighting.EntityControllers
{
    /**
     * Controls hp of entity (taking damage or healing)
     */
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private Slider slider;
        [SerializeField] private Text hpText;

        private int _maxHp;
        private int _hp;
        
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        public bool IsDied => _hp <= 0;

        private void Start()
        {
            _maxHp = FightingSaver.LoadMaxHp();
            
            slider.minValue = 0;
            slider.maxValue = _maxHp;
            slider.value = _maxHp;
            slider.wholeNumbers = true;
            _hp = _maxHp;
            hpText.text = _hp.ToString();
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
            _hp = Math.Max(_hp, 0);
            slider.value = _hp;
            hpText.text = _hp.ToString();
            animator.SetTrigger(Hurt);
        }

        public void Heal(int amount)
        {
            _hp += amount;
            _hp = Math.Min(_hp, _maxHp);
            slider.value = _hp;
            hpText.text = _hp.ToString();
        }
        
    }
}