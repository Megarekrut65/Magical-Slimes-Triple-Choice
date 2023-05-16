using System;
using Global.DescriptionBox;
using UnityEngine;

namespace IncrementalMode.AutoFarming
{
    [Serializable]
    public class FarmInfo
    {
        public Sprite icon;
        public string key;
        public int level;
        public ulong startPrice;
        public int priceFunctionIndex;
        public ulong startAmount;
        public int amountFunctionIndex;
    }
}