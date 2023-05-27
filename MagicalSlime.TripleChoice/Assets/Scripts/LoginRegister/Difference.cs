using System.Collections.Generic;
using FightingMode;
using Global;

namespace LoginRegister
{
    /// <summary>
    /// Compares data from local storage and database for equality.
    /// </summary>
    public static class Difference
    {
        public static bool IsDifference(Dictionary<string, object> data)
        {
            Info info = DictionaryToInfo.Get(data);
            Info local = LocalStorageInfo.Get();

            return !local.Equals(info);
        }
    }
}