using System;
using System.Collections.Generic;
using System.Globalization;
using Fighting;
using Fighting.Game;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using Global.Hats;
using IncrementalMode.AutoFarming;
using IncrementalMode.Shop;
using UnityEngine;

namespace Database
{
    public class DatabaseSaver
    {

        public void SaveUserData(string id, Action<bool, string> answer)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            DocumentReference docRef = db.Collection("users").Document(id);

            docRef.SetAsync(GetAllData(), SetOptions.MergeAll)
                .ContinueWithOnMainThread(task =>
                {
                    Debug.Log(task.Exception?.Message);
                    answer(task.IsCompletedSuccessfully, task.Exception?.Message ?? "");
                });
        }

        public Dictionary<string, object> GetAllData()
        {
            Dictionary<string, object> data = GetUserData();
            data["currentSlime"] = GetCurrentSlimeData();
            data["lastSave"] = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            return data;
        }
        private Dictionary<string, object> GetUserData()
        {
            return new Dictionary<string, object>
            {
                { "maxLevel", DataSaver.LoadMaxLevelForAccount() },
                { "maxEnergy", DataSaver.LoadMaxEnergyForAccount().ToString()},
                { "slimes", DataSaver.LoadSlimeDataJson()  },
                { "diamonds", DataSaver.LoadDiamonds()  },
                { "cups", FightingSaver.LoadCups() },
                { "hats", GetHats()},
                { "currentHat", DataSaver.LoadCurrentHat()}
            };
        }
        private Dictionary<string, object> GetCurrentSlimeData()
        {
            Dictionary<string, object> currentSlime = new Dictionary<string, object>
            {
                {"slimeName", DataSaver.LoadSlimeName()},
                {"slimeType", DataSaver.LoadSlimeType()},
                {"energy", DataSaver.LoadEnergy().ToString()},
                {"level", DataSaver.LoadLevel()},
                {"experience", DataSaver.LoadExperience()},
                {"hp", DataSaver.LoadHp()},
                {"autoFarms", GetAutoFarms()},
                {"shops", GetShops()}
            };
            return currentSlime;
        }
        private Dictionary<string, object> GetShops()
        {
            Dictionary<string, object> shops = new Dictionary<string, object>();
            List<Tuple<string,int>> shopsKey = ShopRegister.ShopKeys;

            foreach (Tuple<string,int> item in shopsKey)
            {
                shops.Add(item.Item1, DataSaver.LoadShop(item.Item1, item.Item2));
            }

            return shops;
        }
    
        private Dictionary<string, object> GetAutoFarms()
        {
            Dictionary<string, object> autoFarms = new Dictionary<string, object>();
            List<string> farmsKey = AutoFarmRegister.AutoFarmingKeys;

            foreach (string key in farmsKey)
            {
                autoFarms.Add(key, DataSaver.LoadAutoFarm(key));
            }

            return autoFarms;
        }
        private Dictionary<string, object> GetHats()
        {
            Dictionary<string, object> hats = new Dictionary<string, object>();
            Hat[] list = HatsList.Hats;

            foreach (Hat hat in list)
            {
                hats.Add(hat.key, DataSaver.LoadHatIsBought(hat.key));
            }

            return hats;
        }
    }
}