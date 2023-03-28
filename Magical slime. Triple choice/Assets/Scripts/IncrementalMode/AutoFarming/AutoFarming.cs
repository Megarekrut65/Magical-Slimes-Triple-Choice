using System;
using System.Collections.Generic;
using Global;
using Global.Localization;
using UnityEngine;

namespace IncrementalMode.AutoFarming
{
    public class AutoFarming : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject farmObject;

        [SerializeField] private FarmInfo[] infos;

        private List<FarmItem> _items = new List<FarmItem>();
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
                item.SetInfo(new Farm(info, id));
                _items.Add(item);
            }

            Entity.GameOverEvent += ClearFarms;
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