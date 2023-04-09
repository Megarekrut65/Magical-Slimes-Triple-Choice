using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.DescriptionBox;
using Global.Localization;
using UnityEngine;

namespace IncrementalMode.AutoFarming
{
    public class AutoFarming : MonoBehaviour
    {
        [SerializeField] private AutoFarmDescriptionBox descriptionBox;
        [SerializeField] private MoneyController moneyController;
        
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject farmObject;

        [SerializeField] private FarmInfo[] infos;

        private readonly List<FarmItem> _items = new List<FarmItem>();
        private void Start()
        {
            foreach (FarmInfo info in infos)
            {

                info.level = DataSaver.LoadAutoFarm(info.key);

                GameObject obj = Instantiate(farmObject, parent, false);
                FarmItem item = obj.GetComponent<FarmItem>();
                item.SetInfo(new Farm(info), descriptionBox, moneyController);
                _items.Add(item);
            }

            StartCoroutine(FarmAmounting());
        }
        private void Awake()
        {
            Entity.OnEntityDied += ClearFarms;
        }

        private void OnDestroy()
        {
            Entity.OnEntityDied -= ClearFarms;
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