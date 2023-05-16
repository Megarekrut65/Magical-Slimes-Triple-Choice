using System;
using UnityEngine;

namespace Global.Hats
{
    [Serializable]
    public class Hat
    {
        public Sprite icon;
        public int price;//in diamonds
        public string key;//for name and description
        public bool isBought;
    }
}