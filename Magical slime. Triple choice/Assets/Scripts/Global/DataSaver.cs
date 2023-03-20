using System;

namespace Global
{
    public class DataSaver
    {
        public static void SaveMoney(ulong amount)
        {
            LocalStorage.SetValue("money", amount.ToString());
        }

        public static void SaveLevel(int level)
        {
            LocalStorage.SetValue("level", level);
        }
        public static void SaveExperience(int experience)
        {
            LocalStorage.SetValue("experience", experience);
        }

        public static void SaveSpeed(float speed)
        {
            LocalStorage.SetValue("speed", speed);
        }
        public static ulong LoadMoney()
        {
            return Convert.ToUInt64(LocalStorage.GetValue("money", "0"));
        }

        public static int LoadLevel()
        {
            return LocalStorage.GetValue("level", 0);
        }
        public static int LoadExperience()
        {
            return LocalStorage.GetValue("experience", 0);
        }
        public static float LoadSpeed()
        {
            return LocalStorage.GetValue("speed", 0f);
        }
    }
}