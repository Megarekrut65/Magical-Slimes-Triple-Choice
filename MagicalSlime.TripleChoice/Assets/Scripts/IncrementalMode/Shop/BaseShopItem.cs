using System;
using System.Numerics;
using Global.DescriptionBox;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Shop
{
    public abstract class BaseShopItem : MonoBehaviour
    {
        [Header("Base Item")]
        [SerializeField] protected Text priceText;
        [SerializeField] protected EnergyController energyController;

        [SerializeField] private DescriptionBox descriptionBox;
        [SerializeField] protected DescriptionItem item;

        private Color _textColor;

        protected virtual void AwakeCall()
        {
            EnergyController.OnMoneyChanged += MoneyChanged;
        }
        protected virtual void OnDestroyCall()
        {
            EnergyController.OnMoneyChanged -= MoneyChanged;
        }
        
        private void Awake()
        {
            AwakeCall();
        }

        private void OnDestroy()
        {
            OnDestroyCall();
        }
        
        private void MoneyChanged(Energy energy)
        {
            priceText.color = BigInteger.Parse(item.price) <= energy.Amount ? _textColor : Color.gray;
        }

        protected virtual void OnStart()
        {
            
        }
        private void Start()
        {
            _textColor = priceText.color;
            
            priceText.text = new Energy(item.price).ToString();
            OnStart();
        }

        protected bool CanBuy()
        {
            return energyController.Buy(new Energy(item.price));
        }
        public abstract void Click();

        public virtual void OpenInfo()
        {
            descriptionBox.ShowBox(item);
        }
    }
}