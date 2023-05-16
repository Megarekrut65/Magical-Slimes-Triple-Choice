using System.Collections.Generic;
using Fighting;
using Fighting.Game;
using Global;

namespace LoginRegister
{
    public static class Difference
    {
        public static bool IsDifference(Dictionary<string, object> data)
        {
            Info info = DictionaryToInfo.Get(data);
            Info local = new Info
            {
                slimeName = DataSaver.LoadSlimeName(),
                maxEnergy = DataSaver.LoadMaxEnergyForAccount().ToString(),
                maxLevel = DataSaver.LoadMaxLevelForAccount(),
                energy = DataSaver.LoadEnergy().ToString(),
                level = DataSaver.LoadLevel(),
                cups = FightingSaver.LoadCups(),
                diamonds = DataSaver.LoadDiamonds()
            };

            return !local.Equals(info);
        }
    }
}