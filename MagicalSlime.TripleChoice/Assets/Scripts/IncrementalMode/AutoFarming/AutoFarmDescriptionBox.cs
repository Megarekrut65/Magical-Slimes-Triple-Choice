using System;
using Global.DescriptionBox;
using Global.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.AutoFarming
{
    public class AutoFarmDescriptionBox : DescriptionBox
    {
        [Header("Auto Farm Description Box")]
        [SerializeField] private StarsController stars;
        
        public void ShowBox(Farm farm)
        {
            base.ShowBox(new DescriptionItem
            {
                key = farm.Info.key,
                price = farm.Price.Amount.ToString(),
                otherTitleKey = "energy-per",
                otherText = farm.Amount.ToString(),
                icon = farm.Info.icon

            });
            stars.SetStars(farm.Info.level);
        }
    }
}