using System;
using System.Collections.Generic;

namespace IncrementalMode.Shop
{
    public static class ShopRegister
    {
        public static readonly List<Tuple<string, int>> ShopKeys = new List<Tuple<string, int>>();

        public static void Register(string key, int val)
        {
            foreach (Tuple<string, int> value in ShopKeys)
            {
                if(value.Item1.Equals(key)) return;
            }
            ShopKeys.Add(new Tuple<string, int>(key,val));
        }
    }
}