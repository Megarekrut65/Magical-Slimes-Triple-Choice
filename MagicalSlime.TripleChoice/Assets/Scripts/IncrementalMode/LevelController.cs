using System;
using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    /// <summary>
    /// Controls entity levels. Level upping and experience adding.  
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Animation levelUpAnim;
        
        [SerializeField] private Slider levelSlider;
        [SerializeField] private Image border;
        [SerializeField] private Text levelText;
        [SerializeField] private Text diamondsText;

        [SerializeField] private Sprite[] borders;

        [SerializeField] private int experiencePeriod;
        [SerializeField] private int levelPeriod;

        [SerializeField] private Entity entity;
        private int MaxExperience => levelPeriod * Level + experiencePeriod;
        private bool _isLevelUpping = false;

        public int Level { get; private set; }

        private void Start()
        {
            levelSlider.minValue = 0;
            Level = DataSaver.LoadLevel();
            SetData();
            levelSlider.value = DataSaver.LoadExperience();
        }
        private void SetData()
        {
            levelSlider.value = 0;
            levelSlider.maxValue = MaxExperience;
            levelText.text = Level.ToString();
            border.sprite = borders[Math.Min(borders.Length - 1, Level / levelPeriod)];
            _isLevelUpping = false;
        }
        public void Click()
        {
            levelSlider.value += 1;
            DataSaver.SaveExperience((int)levelSlider.value);
            if (!_isLevelUpping && levelSlider.value >= levelSlider.maxValue) LevelUp();
        }

        private void LevelUp()
        {
            _isLevelUpping = true;
            Level++;

            entity.Heal(Level % levelPeriod == 0?25:5);
            if (Level % levelPeriod == 0)
            {
                int diamonds = DataSaver.LoadDiamonds() + 1;
                DataSaver.SaveDiamonds(diamonds);
                diamondsText.text = diamonds.ToString();
            }
            levelSlider.value = 0;
            DataSaver.SaveExperience((int)levelSlider.value);
            
            DataSaver.SaveLevel(Level);
            levelUpAnim.Play("LevelUp");
        }
    }
}