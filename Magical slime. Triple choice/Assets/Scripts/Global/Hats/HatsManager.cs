using System;
using Account;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Hats
{
    public class HatsManager : MonoBehaviour
    {
        [SerializeField] private GameObject hatObject;
        [SerializeField] private Transform hatsPlace;
        [SerializeField] private Hat[] hats;

        [SerializeField] private SpriteRenderer currentHat;

        [SerializeField] private DiamondsManager diamondsManager;
        
        private void Start()
        {
            string current = DataSaver.LoadCurrentHat();
            if (current != "")
            {
                foreach (Hat hat in hats)
                {
                    if (hat.key != current) continue;
                    currentHat.sprite = hat.icon;
                    break;
                }
            }

            foreach (Hat hat in hats)
            {
                hat.isBought = hat.key == "none" || DataSaver.LoadHatIsBought(hat.key);
                GameObject obj = Instantiate(hatObject, hatsPlace, false);
                HatItem item = obj.GetComponent<HatItem>();
                item.SetHat(hat, currentHat, diamondsManager);
            }
        }
    }
}