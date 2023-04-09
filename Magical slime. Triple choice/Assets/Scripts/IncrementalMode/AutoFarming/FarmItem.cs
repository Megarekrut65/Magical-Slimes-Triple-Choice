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

        private Color _textColor;
        
        private Farm _farm;
        private AutoFarmDescriptionBox _descriptionBox;
        private EnergyController _energyController;


        private void Start()
        {
            _textColor = priceText.color;
        }

        public void SetInfo(Farm farm, AutoFarmDescriptionBox descriptionBox, EnergyController energyController)
        {
            _farm = farm;
            _descriptionBox = descriptionBox;
            _energyController = energyController;
            LoadData();
        }

        private void Awake()
        {
            EnergyController.OnMoneyChanged += MoneyChanged;
        }

        private void OnDestroy()
        {
            EnergyController.OnMoneyChanged -= MoneyChanged;
        }

        private void MoneyChanged(Energy energy)
        {
            if(_farm == null) return;

            priceText.color = _farm.Price.Amount <= energy.Amount ? _textColor : Color.gray;
        }
        public void ShowDescription()
        {
            _descriptionBox.ShowBox(_farm);
        }
        private void LoadData()
        {
            starsController.SetStars(_farm.Info.level);
            icon.sprite = _farm.Info.icon;
            priceText.text = _farm.Price.ToString();
        }
        public void LevelUp()
        {
            if(_farm == null || _farm.Info.level >= starsController.MaxLevel 
                             || !_energyController.Buy(_farm.Price)) return;

            _farm.Info.level++;
            DataSaver.SaveAutoFarm(_farm.Info.key, _farm.Info.level);
            LoadData();
        }

        public void ClearLevel()
        {
            _farm.Info.level = 0;
            DataSaver.SaveAutoFarm(_farm.Info.key, 0);
        }

        public Energy GetAmount()
        {
            return _farm == null ? new Energy(0) : _farm.Amount;
        }
    }
}