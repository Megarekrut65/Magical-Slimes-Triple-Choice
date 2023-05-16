using System;
using System.Collections.Generic;
using System.Numerics;
using Account.SlimesList;
using Fighting.Game;
using Global;
using Global.Json;
using UnityEngine;

namespace LoginRegister
{
    public static class DictionaryToInfo
    {
        public static Info Get(Dictionary<string, object> data)
        {
             Info info = new Info();
            
            LoadCurrentSlimeData(info, data["currentSlime"] as Dictionary<string, object>);
            LoadUserData(info, data);

            return info;
        }
        private static void LoadCurrentSlimeData(Info info, Dictionary<string, object> data)
        {
            info.slimeName = data["slimeName"] as string;
            info.level = Convert.ToInt32(data["level"]);
            info.energy = data["energy"] as string ?? "0";
        }
        private static void LoadUserData(Info info, Dictionary<string, object> data)
        {
            info.maxEnergy = data["maxEnergy"] as string;
            info.maxLevel = Convert.ToInt32(data["maxLevel"]);
            info.diamonds = Convert.ToInt32(data["diamonds"]);
            info.cups = Convert.ToInt32(data["cups"]);
        }
    }
}