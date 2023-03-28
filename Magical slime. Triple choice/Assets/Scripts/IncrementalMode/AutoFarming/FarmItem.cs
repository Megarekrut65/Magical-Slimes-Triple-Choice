using System;
using Global;
using Global.Localization;
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
        private DescriptionBox _descriptionBox;
        private MoneyController _moneyController;


        private void Start()
        {
            _textColor = priceText.color;
        }

        public void SetInfo(Farm farm, DescriptionBox descriptionBox, MoneyController moneyController)
        {
            _farm = farm;
            _descriptionBox = descriptionBox;
            _moneyController = moneyController;
            LoadData();
        }

        private void Awake()
        {
            LocalizationManager.Instance.OnLanguageChanged += Localization;
            MoneyController.OnMoneyChanged += MoneyChanged;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLanguageChanged -= Localization;
            MoneyController.OnMoneyChanged -= MoneyChanged;
        }

        private void MoneyChanged(Money money)
        {
            if(_farm == null) return;

            priceText.color = _farm.Price.Amount <= money.Amount ? _textColor : Color.gray;
        }
        private void Localization()
        {
            if(_farm == null) return;
            
            _farm.Info.title = LocalizationManager.TranslateWord(_farm.Id);
            _farm.Info.description = LocalizationManager.TranslateWord(_farm.Id + "-description");
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
                             || !_moneyController.Buy(_farm.Price)) return;

            _farm.Info.level++;
            LocalStorage.SetValue(_farm.Id, _farm.Info.level);
            LoadData();
        }

        public void ClearLevel()
        {
            _farm.Info.level = 0;
            LocalStorage.SetValue(_farm.Id, 0);
        }

        public Money GetAmount()
        {
            return _farm == null ? new Money(0) : _farm.Amount;
        }
    }
}