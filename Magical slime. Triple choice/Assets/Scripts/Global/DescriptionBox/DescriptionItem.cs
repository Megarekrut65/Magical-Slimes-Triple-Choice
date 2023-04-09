using System;
using System.Numerics;
using UnityEngine;

namespace Global.DescriptionBox
{
    [Serializable]
    public class DescriptionItem
    {
        public string key;
        public BigInteger price;
        public Sprite icon;

        public string otherTitleKey;
        public string otherText;
    }
}