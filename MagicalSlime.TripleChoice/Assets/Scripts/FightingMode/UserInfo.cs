using System;
using System.Collections.Generic;

namespace FightingMode
{
    [Serializable]
    public class UserInfo
    {
        
        public string name;
        public int cups;
        public int maxLevel;
        public string slimeName;
        public string slimeType;
        public string hat;

        public UserInfo()
        {
            
        }

        public UserInfo(string name, int cups, int maxLevel, string slimeName, string slimeType, string hat)
        {
            this.name = name;
            this.cups = cups;
            this.maxLevel = maxLevel;
            this.slimeName = slimeName;
            this.slimeType = slimeType;
            this.hat = hat;
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                {"name", name},
                {"cups", cups},
                {"maxLevel", maxLevel},
                {"slimeName", slimeName},
                {"slimeType", slimeType},
                {"hat", hat}
            };
        }

        public static UserInfo FromDictionary(Dictionary<string, object> data)
        {
            if (data == null) return null;
            
            return new UserInfo
            {
                name = data["name"] as string,
                cups = Convert.ToInt32(data["cups"]),
                maxLevel = Convert.ToInt32(data["maxLevel"]),
                slimeName = data["slimeName"] as string,
                slimeType = data["slimeType"] as string,
                hat = data["hat"] as string
            };
        }
        
    }
}