using System;
using IncrementalMode;
using UnityEngine;

namespace Global.DescriptionBox
{
    [Serializable]
    public class DescriptionItem
    {
        public string key;
        public ulong price;
        public Sprite icon;

        public string otherTitleKey;
        public string otherText;
    }
}