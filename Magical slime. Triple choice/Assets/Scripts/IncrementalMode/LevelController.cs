using System;
using System.Collections.Generic;
using Global;
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

        private void Awake()
        {
            Entity.OnEntityDied += Die;
        }

        private void OnDestroy()
        {
            Entity.OnEntityDied -= Die;
        }

        private void Die()
        {
            Level = DataSaver.LoadLevel();
            DataSaver.SaveLevel(Level/2);
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
            DataSaver.SaveLevel(Level);
            levelUpAnim.Play("LevelUp");
        }
    }
}