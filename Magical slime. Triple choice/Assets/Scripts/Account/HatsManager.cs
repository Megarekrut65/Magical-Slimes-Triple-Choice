using Global;
using Global.Hats;
using UnityEngine;

namespace Account
{
    public class HatsManager : MonoBehaviour
    {
        [SerializeField] private GameObject hatObject;
        [SerializeField] private Transform hatsPlace;

        [SerializeField] private SpriteRenderer currentHat;

        [SerializeField] private DiamondsManager diamondsManager;

        [SerializeField] private BuyManager buyManager;
        
        private Hat[] _hats;
        
        private void Start()
        {
            _hats = HatsList.Hats;
            HatItem[] items = new HatItem[_hats.Length]; 
            for(int i = 0; i < _hats.Length; i++)
            {
                Hat hat = _hats[i];
                
                hat.isBought = hat.key == "none" || DataSaver.LoadHatIsBought(hat.key);
                GameObject obj = Instantiate(hatObject, hatsPlace, false);
                HatItem item = obj.GetComponent<HatItem>();
                items[i] = item;
                
                item.SetHat(i, hat, currentHat, diamondsManager, buyManager);
            }
            buyManager.SetHats(items);
            buyManager.Click(-1);
        }
    }
}