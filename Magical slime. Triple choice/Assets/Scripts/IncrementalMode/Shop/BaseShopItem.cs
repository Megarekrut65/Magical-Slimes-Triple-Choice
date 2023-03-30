using System;
using Global.DescriptionBox;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Shop
{
    public abstract class BaseShopItem : MonoBehaviour
    {
        [Header("Base Item")]
        [SerializeField] protected Text priceText;
        [SerializeField] protected MoneyController moneyController;

        [SerializeField] private DescriptionBox descriptionBox;
        [SerializeField] protected DescriptionItem item;

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
            priceText.color = item.price <= money.Amount ? _textColor : Color.gray;
        }

        protected virtual void OnStart()
        {
            
        }
        private void Start()
        {
            _textColor = priceText.color;
            
            priceText.text = new Money(item.price).ToString();
            OnStart();
        }

        protected bool CanBuy()
        {
            return moneyController.Buy(new Money(item.price));
        }
        public abstract void Click();

        public virtual void OpenInfo()
        {
            descriptionBox.ShowBox(item);
        }
    }
}