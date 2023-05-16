using Global.Hats;
using UnityEngine;
using UnityEngine.UI;

namespace Account
{
    public class BuyManager : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Text diamonds;
        
        private int _currentIndex = -1;
        private HatItem[] _items;

        public void SetHats(HatItem[] items)
        {
            _items = items;
        }

        public void Buy()
        {
            if(_currentIndex == -1 || _items == null || _currentIndex >= _items.Length) return;
            
            bool res = _items[_currentIndex].Buy();
            
            gameObject.SetActive(res);
        }

        public void Click(int index)
        {
            panel.SetActive(false);
            _currentIndex = index;
            if(_currentIndex == -1 || _items == null || _currentIndex >= _items.Length) return;

            panel.SetActive(!_items[_currentIndex].IsBought);
            diamonds.text = _items[_currentIndex].Price.ToString();
        }
    }
}