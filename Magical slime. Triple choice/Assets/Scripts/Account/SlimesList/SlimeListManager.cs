using Global;
using Global.Entity;
using UnityEngine;

namespace Account.SlimesList
{
    public class SlimeListManager:MonoBehaviour
    {
        [SerializeField] private Color background1;
        [SerializeField] private Color background2;

        [SerializeField] private Transform place;
        [SerializeField] private GameObject listObject;

        private void Start()
        {
            SlimeData[] data = DataSaver.LoadSlimeData();
            if(data == null) return;

            int i = 0;
            foreach (SlimeData slimeData in data)
            {
                GameObject obj = Instantiate(listObject, place, false);
                SlimesItem item = obj.GetComponent<SlimesItem>();
                item.SetData(slimeData, EntityList.GetEntity(slimeData.key)?.idleController, 
                    i % 2 == 0?background1:background2);
                i++;
            }
        }
    }
}