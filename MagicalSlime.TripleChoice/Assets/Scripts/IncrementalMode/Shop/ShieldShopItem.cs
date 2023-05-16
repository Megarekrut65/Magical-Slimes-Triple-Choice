using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Shop
{
    public class ShieldShopItem : TimeShopItem
    {
        [Header("Shield Item")] 
        [SerializeField] private ShapeController shapeController;

        [SerializeField] private Image hpFill;
        [SerializeField] private Color shieldColor;
        private Color _hpColor;

        protected override void OnStart()
        {
            _hpColor = hpFill.color;
            base.OnStart();
        }

        public override void Click()
        {
            if(!CanBuy()) return;
            StartTime();
        }

        protected override void OnTimeBegin()
        {
            hpFill.color = shieldColor;
            shapeController.IsShield = true;
        }

        protected override void OnTimeEnd()
        {
            hpFill.color = _hpColor;
            shapeController.IsShield = false;
        }

        protected override void TimeTick(int time)
        {
            base.TimeTick(time);
            item.otherText = (time+1).ToString();
        }
    }
}