using System;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Shop
{
    public abstract class BaseShopItem : MonoBehaviour
    {
        [SerializeField] protected Text priceText;
        [SerializeField] protected MoneyController moneyController;
        [SerializeField] protected ulong price;

        private Color _textColor;
        private void Awake()
        {
            MoneyController.OnMoneyChanged += MoneyChanged;
        }

        private void OnDestroy()
        {
            MoneyController.OnMoneyChanged -= MoneyChanged;
        }

        private void MoneyChanged(Money money)
        {
            priceText.color = price <= money.Amount ? _textColor : Color.gray;
        }
        private void Start()
        {
            _textColor = priceText.color;
            
            priceText.text = new Money(price).ToString();
        }

        protected bool CanBuy()
        {
            return moneyController.Buy(new Money(price));
        }
        public abstract void Click();
    }
}