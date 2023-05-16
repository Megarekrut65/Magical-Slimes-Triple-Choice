using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Account.SlimesList;
using Global.Json;
using UnityEngine;

namespace LoginRegister
{
    public static class Merger
    {
        public static Dictionary<string, object> Merge(Dictionary<string, object> accountData, 
            Dictionary<string, object> deviceData)
        {
            Dictionary<string, object> data = deviceData;
            data["username"] = accountData["username"];
            data["registrationDate"] = accountData["registrationDate"];
            data["lastSave"] = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            
            data["diamonds"] = accountData["diamonds"];
            data["hats"] = accountData["hats"];
            
            data["maxLevel"] = Math.Max(Convert.ToInt32(accountData["maxLevel"]),
                Convert.ToInt32(deviceData["maxLevel"]));
            data["maxEnergy"] = BigInteger.Max(BigInteger.Parse(accountData["maxEnergy"] as string ?? "0"),
                BigInteger.Parse(deviceData["maxEnergy"] as string ?? "0"));
            
            ItemData<SlimeData[]> listAccount = 
                JsonUtility.FromJson<ItemData<SlimeData[]>>(accountData["slimes"] as string);
            
            ItemData<SlimeData[]> listDevice = 
                JsonUtility.FromJson<ItemData<SlimeData[]>>(deviceData["slimes"] as string);

            HashSet<SlimeData> set = new HashSet<SlimeData>(listAccount.value);
            foreach (SlimeData slime in listDevice.value)
            {
                set.Add(slime);
            }

            ItemData<SlimeData[]> list = new ItemData<SlimeData[]> { key = "list", value = set.ToArray() };
            data["slimes"] = JsonUtility.ToJson(list);

            return data;
        }
    }
}