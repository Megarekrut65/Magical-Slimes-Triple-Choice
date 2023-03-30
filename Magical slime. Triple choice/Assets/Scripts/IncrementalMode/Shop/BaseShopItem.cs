using System;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Shop
{
    public abstract class BaseShopItem : MonoBehaviour
    {
        [Header("Base Item")]
        [SerializeField] protected Text priceText;
        [SerializeField] protected MoneyController moneyController;
        [SerializeField] protected ulong price;

        private Color _textColor;

        protected virtual void AwakeCall()
        {
            MoneyController.OnMoneyChanged += MoneyChanged;
        }
        protected virtual void OnDestroyCall()
        {
            MoneyController.OnMoneyChanged -= MoneyChanged;
        }
        
        private void Awake()
        {
            AwakeCall();
        }

        private void OnDestroy()
        {
            OnDestroyCall();
        }
        
        private void MoneyChanged(Money money)
        {
            priceText.color = price <= money.Amount ? _textColor : Color.gray;
        }

        protected virtual void OnStart()
        {
            
        }
        private void Start()
        {
            _textColor = priceText.color;
            
            priceText.text = new Money(price).ToString();
            OnStart();
        }

        protected bool CanBuy()
        {
            return moneyController.Buy(new Money(price));
        }
        public abstract void Click();
    }
}