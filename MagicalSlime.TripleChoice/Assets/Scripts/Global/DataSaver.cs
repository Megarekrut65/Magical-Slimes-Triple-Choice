using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Account.SlimesList;
using Global.Hats;
using Global.Json;
using IncrementalMode;
using IncrementalMode.AutoFarming;
using IncrementalMode.Shop;
using UnityEngine;

namespace Global
{
    public static class DataSaver
    {
        public static DateTime LoadLastSave()
        {
            return Convert.ToDateTime(LocalStorage.GetValue("lastSave", 
            DateTime.MinValue.ToString(CultureInfo.InvariantCulture)));
        }

        public static void LastSave()
        {
            LocalStorage.SetValue("lastSave", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }
        public static void SaveRegistrationDate(string date){
            LocalStorage.SetValue("registrationDate", date);
        }
        public static string LoadRegistrationDate(){
            return LocalStorage.GetValue("registrationDate", "");
        }
        public static void SaveUsername(string username)
        {
            LocalStorage.SetValue("username", username);
        }
        public static string LoadUsername(){
            return LocalStorage.GetValue("username", "");
        }
        public static void SaveLevel(int level)
        {
            LocalStorage.SetValue("level", level);
            SaveMaxLevelForAccount(level);
            LastSave();
        }
        public static void SaveExperience(int experience)
        {
            LocalStorage.SetValue("experience", experience);
            LastSave();
        }

        public static void SaveSpeed(float speed)
        {
            LocalStorage.SetValue("speed", speed);
        }
        public static void SaveEnergy(BigInteger amount)
        {
            LocalStorage.SetValue("energy", amount.ToString());
            SaveMaxEnergy(amount);
            SaveMaxEnergyForAccount(amount);
            LastSave();
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
            LastSave();
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

            List<string> farmsKey = AutoFarmRegister.AutoFarmingKeys;
            foreach (string key in farmsKey)
            {
                LocalStorage.Remove(key + "AutoFarm");
            }
            List<Tuple<string,int>> shopsKey = ShopRegister.ShopKeys;

            foreach (Tuple<string,int> item in shopsKey)
            {
                LocalStorage.Remove(item.Item1 + "Shop");
            }
        }

        public static void RemoveAccountData()
        {
            LocalStorage.Remove("needSave");
            RemoveSlimeData();
            LocalStorage.Remove("username");
            LocalStorage.Remove("registrationDate");
            LocalStorage.Remove("currentHat");
            LocalStorage.Remove("lastSave");
            LocalStorage.Remove("maxLevelAccount");
            LocalStorage.Remove("maxEnergyAccount");
            LocalStorage.Remove("diamonds");
            LocalStorage.Remove("lastSave");
            LocalStorage.Remove("slimeType");
            LocalStorage.Remove("level");

            Hat[] hats = HatsList.Hats;
            foreach (Hat hat in hats)
            {
                LocalStorage.Remove(hat.key + "IsBought");
            }
        }
      

        public static void SaveCurrentHat(string key)
        {
            LocalStorage.SetValue("currentHat", key);
            LastSave();
        }
        public static string LoadCurrentHat()
        {
            return LocalStorage.GetValue("currentHat", "");
        }

        public static void SaveHatIsBought(string key)
        {
            LocalStorage.SetValue(key + "IsBought", "true");
            LastSave();
        }
        public static bool LoadHatIsBought(string key)
        {
            return Convert.ToBoolean(LocalStorage.GetValue(key + "IsBought", "false"));
        }

        public static void SaveAutoFarm(string key, int level)
        {
            LocalStorage.SetValue(key + "AutoFarm", level);
            LastSave();
        }

        public static int LoadAutoFarm(string key)
        {
            return LocalStorage.GetValue(key + "AutoFarm", 0);
        }
        public static void SaveShop(string key, int value)
        {
            LocalStorage.SetValue(key + "Shop", value);
            LastSave();
        }
        public static int LoadShop(string key, int def)
        {
            return LocalStorage.GetValue(key + "Shop", def);
        }
        public static void SaveDiamonds(int diamonds)
        {
            LocalStorage.SetValue("diamonds", diamonds);
            LastSave();
        }
        /// <summary>
        /// Give 25 diamonds once for new user
        /// </summary>
        /// <returns>Saved or new amount of diamonds</returns>
        public static int LoadDiamonds()
        {
            return LocalStorage.GetValue("diamonds", 250);//TODO:make 25 diamonds
        }

        public static SlimeData[] LoadSlimeData()
        {
            ItemData<SlimeData[]> list = JsonUtility.FromJson<ItemData<SlimeData[]>>(
                LoadSlimeDataJson());

            return list.value;
        }
        public static string LoadSlimeDataJson()
        {
            return LocalStorage.GetValue("slimeData", "{\"list\":[]}");
        }

        public static void SaveSlimeData(SlimeData[] data)
        {
            ItemData<SlimeData[]> list = new ItemData<SlimeData[]>{key="list", value = data};
            LocalStorage.SetValue("slimeData", JsonUtility.ToJson(list));
            LastSave();
        }

        public static string LoadSlimeType()
        {
            return LocalStorage.GetValue("slimeType", "blue-slime");
        }

        public static void SaveSlimeType(string type)
        {
            LocalStorage.SetValue("slimeType", type);
        }

        public static void SaveMaxLevelForAccount(int level)
        {
            int maxLevel = LoadMaxLevelForAccount();
            if(level > maxLevel) LocalStorage.SetValue("maxLevelAccount", level);
        }

        public static int LoadMaxLevelForAccount()
        {
            return LocalStorage.GetValue("maxLevelAccount", 0);
        } 
        public static void SaveMaxEnergyForAccount(BigInteger level)
        {
            BigInteger maxLevel = LoadMaxEnergyForAccount();
            if(level > maxLevel) LocalStorage.SetValue("maxEnergyAccount", level.ToString());
        }

        public static BigInteger LoadMaxEnergyForAccount()
        {
            return BigInteger.Parse(LocalStorage.GetValue("maxEnergyAccount", "0"));
        } 
    }
}