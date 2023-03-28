using System;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.AutoFarming
{
    public class FarmItem : MonoBehaviour
    {
        [SerializeField] private Text priceText;
        [SerializeField] private Image icon;

        [SerializeField] private StarsController starsController;
        
        private Farm _farm;

        public void SetInfo(Farm farm)
        {
            _farm = farm;
            LoadData();
        }

        private void LoadData()
        {
            starsController.SetStars(_farm.Info.level);
            icon.sprite = _farm.Info.icon;
            priceText.text = _farm.Price.ToString();
        }
        public void LevelUp()
        {
            if(_farm == null || _farm.Info.level >= starsController.MaxLevel) return;

            _farm.Info.level++;
            LocalStorage.SetValue(_farm.Id, _farm.Info.level);
            LoadData();
        }

        public void ClearLevel()
        {
            _farm.Info.level = 0;
            LocalStorage.SetValue(_farm.Id, 0);
        }
    }
}