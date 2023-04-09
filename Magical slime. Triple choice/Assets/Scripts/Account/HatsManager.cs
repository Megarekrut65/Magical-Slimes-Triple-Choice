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
        
        private Hat[] _hats;
        
        private void Start()
        {
            _hats = HatsList.Hats;

            foreach (Hat hat in _hats)
            {
                hat.isBought = hat.key == "none" || DataSaver.LoadHatIsBought(hat.key);
                GameObject obj = Instantiate(hatObject, hatsPlace, false);
                HatItem item = obj.GetComponent<HatItem>();
                item.SetHat(hat, currentHat, diamondsManager);
            }
        }
    }
}