using Account;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Hats
{
    /// <summary>
    /// Loads to GUI hat data and controls selecting and buying of hats.
    /// </summary>
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
        private BuyManager _buyManager;
        private int _index;
        
        public bool IsBought => _hat.isBought;
        public int Price => _hat.price;

        public void SetHat(int index, Hat hat, SpriteRenderer entityHat, 
            DiamondsManager diamondsManager, BuyManager buyManager)
        {
            _index = index;
            _hat = hat;
            _entityHat = entityHat;
            _diamondsManager = diamondsManager;
            _buyManager = buyManager;
            
            icon.sprite = hat.icon;
            price.text = hat.price.ToString();

            ChangeBackground(hat.isBought);
        }

        private void ChangeBackground(bool isBought)
        {
            if (isBought) background.color = boughtColor;
            pricePlace.SetActive(!isBought);
        }
        public bool Buy()
        {
            if(_hat == null || _diamondsManager == null 
                            || !_diamondsManager.Buy(_hat.price)) return false;
            
            DataSaver.SaveHatIsBought(_hat.key);
            _hat.isBought = true;
            ChangeBackground(true);
            Click();

            return true;
        }
        
        public void Click()
        {
            if (_entityHat == null || _hat == null) return;
            _entityHat.sprite = _hat.icon;
            if(_hat.isBought) DataSaver.SaveCurrentHat(_hat.key);
            
            _buyManager.Click(_index);
        }
    }
}