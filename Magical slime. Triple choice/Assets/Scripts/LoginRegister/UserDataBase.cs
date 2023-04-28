using System;
using System.Collections.Generic;
using System.Globalization;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using IncrementalMode.AutoFarming;
using IncrementalMode.Shop;

namespace LoginRegister
{
    public static class UserDataBase
    {
        public static void AddNewUser(string id, string username, Action<bool, string> answer)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            db.Settings.PersistenceEnabled = false;
            
            DocumentReference docRef = db.Collection("users").Document(id);
            Dictionary<string, object> autoFarms = new Dictionary<string, object>();
            List<string> farmsKey = AutoFarmRegister.AutoFarmingKeys;
            foreach (string key in farmsKey)
            {
                autoFarms.Add(key, DataSaver.LoadAutoFarm(key));
            }
            Dictionary<string, object> shops = new Dictionary<string, object>();
            List<Tuple<string,int>> shopsKey = ShopRegister.ShopKeys;
            foreach (Tuple<string,int> item in shopsKey)
            {
                shops.Add(item.Item1, DataSaver.LoadShop(item.Item1, item.Item2));
            }
            
            Dictionary<string, object> currentSlime = new Dictionary<string, object>
            {
                {"slimeName", DataSaver.LoadSlimeName()},
                {"energy", DataSaver.LoadEnergy()},
                {"level", DataSaver.LoadLevel()},
                {"experience", DataSaver.LoadExperience()},
                {"hp", DataSaver.LoadHp()},
                {"autoFarms", autoFarms},
                {"shops", shops}
            };
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "username", username },
                { "maxLevel", DataSaver.LoadMaxLevelForAccount() },
                { "maxEnergy", DataSaver.LoadMaxEnergyForAccount()  },
                { "registrationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture) },
                { "slimes", DataSaver.LoadSlimeDataJson()  },
                { "diamonds", DataSaver.LoadDiamonds()  },
                { "currentSlime", currentSlime}
            };
            docRef.SetAsync(data).ContinueWithOnMainThread(task =>
            {
                answer(task.IsCompletedSuccessfully, task.Exception?.ToString() ?? "");
            });
        }
    }
}