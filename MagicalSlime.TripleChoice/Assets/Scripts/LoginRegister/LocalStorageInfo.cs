using FightingMode;
using Global;

namespace LoginRegister
{
    public static class LocalStorageInfo
    {
        public static Info Get()
        {
            return new Info
            {
                slimeName = DataSaver.LoadSlimeName(),
                maxEnergy = DataSaver.LoadMaxEnergyForAccount().ToString(),
                maxLevel = DataSaver.LoadMaxLevelForAccount(),
                energy = DataSaver.LoadEnergy().ToString(),
                level = DataSaver.LoadLevel(),
                cups = FightingSaver.LoadCups(),
                diamonds = DataSaver.LoadDiamonds()
            };
        }
    }
}