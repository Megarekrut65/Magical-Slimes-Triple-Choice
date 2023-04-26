using System;
using System.Numerics;
using Account.SlimesList;
using Global.Json;
using IncrementalMode;
using UnityEngine;

namespace Global
{
    public static class DataSaver
    {
        public static void SaveLevel(int level)
        {
            LocalStorage.SetValue("level", level);
        }
        public static void SaveExperience(int experience)
        {
            LocalStorage.SetValue("experience", experience);
        }

        public static void SaveSpeed(float speed)
        {
            LocalStorage.SetValue("speed", speed);
        }
        public static void SaveEnergy(BigInteger amount)
        {
            LocalStorage.SetValue("energy", amount.ToString());
            SaveMaxEnergy(amount);
        }
        public static BigInteger LoadEnergy()
        {
            return BigInteger.Parse(LocalStorage.GetValue("energy", "0"));
        }
        public static void SaveMaxEnergy(BigInteger amount)
        {
            BigInteger max = LoadMaxEnergy();
            if (amount > max) LocalStorage.SetValue("maxEnergy", amount.ToString());
        }
        public static BigInteger LoadMaxEnergy()
        {
            return BigInteger.Parse(LocalStorage.GetValue("maxEnergy", "0"));
        }

        public static int LoadLevel()
        {
            return LocalStorage.GetValue("level", 0);
        }
        public static int LoadExperience()
        {
            return LocalStorage.GetValue("experience", 0);
        }
        public static float LoadSpeed()
        {
            return LocalStorage.GetValue("speed", 0f);
        }
        public static void SaveHp(int hp)
        {
            LocalStorage.SetValue("hp", hp);
        }
        public static int LoadHp()
        {
            return LocalStorage.GetValue("hp", IncrementalMode.Entity.MaxHp);
        }
        public static void SaveShapeTime(int shapeTime)
        {
            LocalStorage.SetValue("shapeTime", shapeTime);
        }
        public static int LoadShapeTime()
        {
            return LocalStorage.GetValue("shapeTime", ShapeController.ShapeTime);
        }

        public static void SaveSlimeName(string slimeName)
        {
            LocalStorage.SetValue("slimeName", slimeName);
        }
        public static string LoadSlimeName()
        {
            return LocalStorage.GetValue("slimeName", "");
        }
        public static void RemoveSlimeData()
        {
            LocalStorage.Remove("shapeTime");
            LocalStorage.Remove("hp");
            LocalStorage.Remove("speed");
            LocalStorage.Remove("energy");
            LocalStorage.Remove("maxEnergy");
            LocalStorage.Remove("experience");
            LocalStorage.Remove("slimeName");
        }

        public static void SaveCurrentHat(string key)
        {
            LocalStorage.SetValue("currentHat", key);
        }
        public static string LoadCurrentHat()
        {
            return LocalStorage.GetValue("currentHat", "");
        }

        public static void SaveHatIsBought(string key)
        {
            LocalStorage.SetValue(key + "IsBought", "true");
        }
        public static bool LoadHatIsBought(string key)
        {
            return Convert.ToBoolean(LocalStorage.GetValue(key + "IsBought", "false"));
        }

        public static void SaveAutoFarm(string key, int level)
        {
            LocalStorage.SetValue(key + "AutoFarm", level);
        }

        public static int LoadAutoFarm(string key)
        {
            return LocalStorage.GetValue(key + "AutoFarm", 0);
        }
        public static void SaveShop(string key, int value)
        {
            LocalStorage.SetValue(key + "Shop", value);
        }
        public static int LoadShop(string key, int def)
        {
            return LocalStorage.GetValue(key + "Shop", def);
        }
        public static void SaveDiamonds(int diamonds)
        {
            LocalStorage.SetValue("diamonds", diamonds);
        }
        public static int LoadDiamonds()
        {
            return LocalStorage.GetValue("diamonds", 15);
        }

        public static SlimeData[] LoadSlimeData()
        {
            ItemData<SlimeData[]> list = JsonUtility.FromJson<ItemData<SlimeData[]>>(
                LocalStorage.GetValue("slimeData", "{\"list\":[]}"));

            return list.value;
        }

        public static void SaveSlimeData(SlimeData[] data)
        {
            ItemData<SlimeData[]> list = new ItemData<SlimeData[]>{key="list", value = data};
            LocalStorage.SetValue("slimeData", JsonUtility.ToJson(list));
        }

        public static string LoadSlimeType()
        {
            return LocalStorage.GetValue("slimeType", "blue-slime");
        }

        public static void SaveSlimeType(string type)
        {
            LocalStorage.SetValue("slimeType", type);
        }
    }
}