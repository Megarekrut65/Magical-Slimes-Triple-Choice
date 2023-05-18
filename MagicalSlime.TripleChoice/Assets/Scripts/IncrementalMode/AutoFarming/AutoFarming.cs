using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using Global.DescriptionBox;
using Global.Localization;
using UnityEngine;
using UnityEngine.Serialization;

namespace IncrementalMode.AutoFarming
{
    /// <summary>
    /// Loads auto farms to GUI. Also counts all profit from auto farms and adds it to player energy.
    /// </summary>
    public class AutoFarming : MonoBehaviour
    {
        [SerializeField] private AutoFarmDescriptionBox descriptionBox;
        [SerializeField] private EnergyController energyController;
        
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject farmObject;

        [SerializeField] private FarmInfo[] infos;

        private readonly List<FarmItem> _items = new List<FarmItem>();
        private void Start()
        {
            foreach (FarmInfo info in infos)
            {
                AutoFarmRegister.Register(info.key);
                info.level = DataSaver.LoadAutoFarm(info.key);

                GameObject obj = Instantiate(farmObject, parent, false);
                FarmItem item = obj.GetComponent<FarmItem>();
                item.SetInfo(new Farm(info), descriptionBox, energyController);
                _items.Add(item);
            }

            StartCoroutine(FarmAmounting());
        }
        private IEnumerator FarmAmounting()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Energy energy = new Energy(0);
                foreach (FarmItem item in _items)
                {
                    energy.Add(item.GetAmount().Amount);
                }
                energyController.AddMoney(energy);
            }
        }
    }
}