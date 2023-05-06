using Global;

namespace Fighting
{
    public static class FightingSaver
    {
        public static int LoadMaxHp()
        {
            return LocalStorage.GetValue("maxHp", 100);
        }

        public static void SaveMaxHp(int maxHp)
        {
            LocalStorage.SetValue("maxHp", maxHp);
        }
    }
}