using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode.Shop
{
    public abstract class ActiveShopItem : BaseShopItem
    {
        [Header("Active Item")]
        [SerializeField] private Outline border;
        private Color _activeColor, _passiveColor;

        protected override void OnStart()
        {
            _activeColor = border.effectColor;
            _passiveColor = new Color(0, 0, 0, 0);
        }

        protected void ActiveOn()
        {
            if(border == null) return;
            border.effectColor = _activeColor;
        }

        protected void ActiveOff()
        {
            if(border == null) return;
            border.effectColor = _passiveColor;
        }
    }
}