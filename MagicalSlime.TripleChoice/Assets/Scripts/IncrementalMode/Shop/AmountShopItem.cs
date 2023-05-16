using System.Collections;
using System.Globalization;
using Global;
using Global.Sound;
using UnityEngine;

namespace IncrementalMode.Shop
{
    public class AmountShopItem : TimeShopItem
    {
        [Header("Amount item")]
        [SerializeField] private SpeedController speedController;

        [SerializeField] private float increaseAmount;

        public override void Click()
        {
            if(!CanBuy()) return;
            StartTime();
        }

        protected override void OnTimeBegin()
        {
            speedController.IncreasePercent *= increaseAmount;
        }

        protected override void OnTimeEnd()
        {
            speedController.IncreasePercent /= increaseAmount;
        }

        public override void OpenInfo()
        {
            item.otherText = $"{increaseAmount:0.0}";
            base.OpenInfo();
        }
    }
}