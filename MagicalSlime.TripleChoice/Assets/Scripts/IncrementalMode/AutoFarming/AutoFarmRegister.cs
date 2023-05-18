using System.Collections.Generic;

namespace IncrementalMode.AutoFarming
{
    /// <summary>
    /// Adds key of auto farm to static list. Uses for controls auto farm keys. Like for adding data to database. 
    /// </summary>
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