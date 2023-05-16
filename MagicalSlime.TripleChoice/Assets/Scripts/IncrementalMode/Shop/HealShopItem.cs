using UnityEngine;

namespace IncrementalMode.Shop
{
    public class HealShopItem : BaseShopItem
    {
        [Header("Heal item")]
        [SerializeField] private int healValue;
        [SerializeField] private Entity entity;
        
        public override void Click()
        {
            if(!CanBuy()) return;
            
            entity.Heal(healValue);
        }

        public override void OpenInfo()
        {
            item.otherText = healValue.ToString();
            base.OpenInfo();
        }
    }
}