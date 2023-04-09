using Account;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Hats
{
    public class HatItem:MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text price;
        [SerializeField] private Image background;
        [SerializeField] private GameObject pricePlace;

        [SerializeField] private Color boughtColor;

        private Hat _hat;
        private SpriteRenderer _entityHat;
        private DiamondsManager _diamondsManager;

        public void SetHat(Hat hat, SpriteRenderer entityHat, DiamondsManager diamondsManager)
        {
            _hat = hat;
            _entityHat = entityHat;
            _diamondsManager = diamondsManager;
            
            icon.sprite = hat.icon;
            price.text = hat.price.ToString();

            ChangeBackground(hat.isBought);
        }

        private void ChangeBackground(bool isBought)
        {
            if (isBought) background.color = boughtColor;
            pricePlace.SetActive(!isBought);
        }
        public void Buy()
        {
            if(_hat == null || _diamondsManager == null 
                            || !_diamondsManager.Buy(_hat.price)) return;
            
            DataSaver.SaveHatIsBought(_hat.key);
            _hat.isBought = true;
            ChangeBackground(true);
            Click();
        }

        public void Click()
        {
            if (_entityHat == null || _hat == null) return;
            _entityHat.sprite = _hat.icon;
            if(_hat.isBought) DataSaver.SaveCurrentHat(_hat.key);
        }
    }
}