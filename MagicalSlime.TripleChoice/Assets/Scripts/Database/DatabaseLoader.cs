using System;
using System.Collections.Generic;
using System.Numerics;
using Account.SlimesList;
using Fighting;
using Fighting.Game;
using Global;
using Global.Json;
using UnityEngine;

namespace Database
{
    public class DatabaseLoader
    {
        public void LoadData(Dictionary<string, object> data)
        {
            if(data == null) return;
            
            LoadCurrentSlimeData(data["currentSlime"] as Dictionary<string, object>);
            LoadUserData(data);
            LoadHats(data["hats"] as Dictionary<string, object>);
            LoadSlimesData(data["slimes"] as string);
        }

        private void LoadSlimesData(string data)
        {
            if(data == null) return;
            ItemData<SlimeData[]> list = JsonUtility.FromJson<ItemData<SlimeData[]>>(data);

            DataSaver.SaveSlimeData(list.value);
        }

        private void LoadCurrentSlimeData(Dictionary<string, object> data)
        {
            if(data == null) return;

            string slimeName = data["slimeName"] as string;
            string slimeType = data["slimeType"] as string;
            int level = Convert.ToInt32(data["level"]);
            BigInteger energy = BigInteger.Parse(data["energy"] as string ?? "0");
            int experience = Convert.ToInt32(data["experience"]);
            int hp = Convert.ToInt32(data["hp"]);

            DataSaver.SaveSlimeName(slimeName);
            DataSaver.SaveSlimeType(slimeType);
            DataSaver.SaveLevel(level);
            DataSaver.SaveEnergy(energy);
            DataSaver.SaveExperience(experience);
            DataSaver.SaveHp(hp);
            
            LoadShopData(data["shops"] as Dictionary<string, object>);
            LoadAutoFarm(data["autoFarms"] as Dictionary<string, object>);
        }
        private void LoadShopData(Dictionary<string, object> data)
        {
            if(data == null) return;

            foreach (KeyValuePair<string, object> item in data)
            {
                DataSaver.SaveShop(item.Key, Convert.ToInt32(item.Value));
            }
        }
        private void LoadAutoFarm(Dictionary<string, object> data)
        {
            if(data == null) return;
            
            foreach (KeyValuePair<string, object> item in data)
            {
                DataSaver.SaveAutoFarm(item.Key, Convert.ToInt32(item.Value));
            }
        }
        private void LoadHats(Dictionary<string, object> data)
        {
            if(data == null) return;
            
            foreach (KeyValuePair<string, object> item in data)
            {
                bool isBought = Convert.ToBoolean(item.Value);
                if(isBought) DataSaver.SaveHatIsBought(item.Key);
            }
        }
        private void LoadUserData(Dictionary<string, object> data)
        {
            string username = data["username"] as string;
            string registrationDate = data["registrationDate"] as string;
            string maxEnergy = data["maxEnergy"] as string;
            int maxLevel = Convert.ToInt32(data["maxLevel"]);
            int diamonds = Convert.ToInt32(data["diamonds"]);
            int cups = Convert.ToInt32(data["cups"]);
            string currentHat = data["currentHat"] as string;

            DataSaver.SaveUsername(username);
            DataSaver.SaveRegistrationDate(registrationDate);
            DataSaver.SaveMaxEnergyForAccount(BigInteger.Parse(maxEnergy??"0"));
            DataSaver.SaveMaxLevelForAccount(maxLevel);
            DataSaver.SaveDiamonds(diamonds);
            FightingSaver.SaveCups(cups);
            DataSaver.SaveCurrentHat(currentHat);
        }
    }
}