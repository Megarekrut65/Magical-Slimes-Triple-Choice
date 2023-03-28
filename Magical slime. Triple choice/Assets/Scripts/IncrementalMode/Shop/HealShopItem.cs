using UnityEngine;

namespace IncrementalMode.Shop
{
    public class HealShopItem : BaseShopItem
    {
        [SerializeField] private int healValue;
        [SerializeField] private Entity entity;
        
        public override void Click()
        {
            if(!CanBuy()) return;
            
            entity.Heal(healValue);
        }
    }
}