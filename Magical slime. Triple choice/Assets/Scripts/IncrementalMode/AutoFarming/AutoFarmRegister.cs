using System.Collections.Generic;

namespace IncrementalMode.AutoFarming
{
    public static class AutoFarmRegister
    {
        public static readonly List<string> AutoFarmingKeys = new List<string>();

        public static void Register(string key)
        {
            foreach (string value in AutoFarmingKeys)
            {
                if(value.Equals(key)) return;
            }
            AutoFarmingKeys.Add(key);
        }
    }
}