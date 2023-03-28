using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.Localization;
using UnityEngine;

namespace IncrementalMode.AutoFarming
{
    public class AutoFarming : MonoBehaviour
    {
        [SerializeField] private DescriptionBox descriptionBox;
        [SerializeField] private MoneyController moneyController;
        
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject farmObject;

        [SerializeField] private FarmInfo[] infos;

        private readonly List<FarmItem> _items = new List<FarmItem>();
        private void Start()
        {
            foreach (FarmInfo info in infos)
            {
                string id = info.title;
                
                info.level = LocalStorage.GetValue(info.title, 0);
                info.title = LocalizationManager.Instance.GetWord(info.title);
                info.description = LocalizationManager.Instance.GetWord(info.description);
                
                GameObject obj = Instantiate(farmObject, parent, false);
                FarmItem item = obj.GetComponent<FarmItem>();
                item.SetInfo(new Farm(info, id), descriptionBox, moneyController);
                _items.Add(item);
            }

            Entity.OnEntityDied += ClearFarms;

            StartCoroutine(FarmAmounting());
        }

        private IEnumerator FarmAmounting()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Money money = new Money(0);
                foreach (FarmItem item in _items)
                {
                    money.Add(item.GetAmount().Amount);
                }
                moneyController.AddMoney(money);
            }
        }

        private void ClearFarms()
        {
            foreach (FarmItem item in _items)
            {
                item.ClearLevel();
            }
        }
    }
}