using System;
using Global;
using UnityEngine;

namespace CreatingSlime
{
    public class TypeManager : MonoBehaviour
    {
        [SerializeField] private TypeItem[] pins;

        private void Start()
        {
            Click("blue-slime");
        }

        public void Click(string type)
        {
            DataSaver.SaveSlimeType(type);
            foreach (TypeItem item in pins)
            {
                item.obj.SetActive(item.type == type);
            }
        }
    }
}