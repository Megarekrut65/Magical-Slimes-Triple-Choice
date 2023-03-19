using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Animation levelUpAnim;
        
        [SerializeField] private Slider levelSlider;
        [SerializeField] private Image border;
        [SerializeField] private Text levelText;

        [SerializeField] private Sprite[] borders;

        [SerializeField] private int experiencePeriod;
        [SerializeField] private int levelPeriod;
        private int MaxExperience => levelPeriod * _level + experiencePeriod;
        private int _level;
        private bool _isLevelUpping = false;
        

        private void Start()
        {
            levelSlider.minValue = 0;
            SetData();
        }

        private void SetData()
        {
            levelSlider.value = 0;
            levelSlider.maxValue = MaxExperience;
            levelText.text = _level.ToString();
            border.sprite = borders[Math.Min(borders.Length - 1, _level / levelPeriod)];
            _isLevelUpping = false;
        }
        public void Click()
        {
            levelSlider.value += 1;
            if (!_isLevelUpping && levelSlider.value >= levelSlider.maxValue) LevelUp();
        }

        private void LevelUp()
        {
            _isLevelUpping = true;
            _level++;
            levelUpAnim.Play("LevelUp");
        }
    }
}