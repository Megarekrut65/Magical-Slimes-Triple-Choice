using System;
using System.Collections.Generic;
using System.Globalization;
using FightingMode;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using Global.Hats;
using IncrementalMode.AutoFarming;
using IncrementalMode.Shop;
using UnityEngine;

namespace DataManagement
{
    /// <summary>
    /// Gets user data from database or local storage.
    /// </summary>
    public static class UserData
    {
        public static void GetUserDataFromDatabase(string id, Action<bool, Dictionary<string, object>> answer)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("users").Document(id);

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                CustomLogger.Log(task.Exception?.Message);
                if (task.IsFaulted)
                {
                    answer(false, null);
                    return;
                }
                DocumentSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    answer(true, snapshot.ToDictionary());
                    return;
                }
                answer(false, null);
            });
        }
        public static Dictionary<string, object> GetUserDataFromLocalStorage()
        {
            Dictionary<string, object> data = GetAccountData();
            data["currentSlime"] = GetCurrentSlimeData();
            data["lastSave"] = DateTimeUtc.NowInvariant;

            return data;
        }
        private static Dictionary<string, object> GetAccountData()
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
        private static Dictionary<string, object> GetCurrentSlimeData()
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
        private static Dictionary<string, object> GetShops()
        {
            Dictionary<string, object> shops = new Dictionary<string, object>();
            List<Tuple<string,int>> shopsKey = ShopRegister.ShopKeys;

            foreach (Tuple<string,int> item in shopsKey)
            {
                shops.Add(item.Item1, DataSaver.LoadShop(item.Item1, item.Item2));
            }

            return shops;
        }
    
        private static Dictionary<string, object> GetAutoFarms()
        {
            Dictionary<string, object> autoFarms = new Dictionary<string, object>();
            List<string> farmsKey = AutoFarmRegister.AutoFarmingKeys;

            foreach (string key in farmsKey)
            {
                autoFarms.Add(key, DataSaver.LoadAutoFarm(key));
            }

            return autoFarms;
        }
        private static Dictionary<string, object> GetHats()
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