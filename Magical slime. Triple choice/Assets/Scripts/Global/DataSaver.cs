using System;
using IncrementalMode;

namespace Global
{
    public static class DataSaver
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
        public static void SaveHp(int hp)
        {
            LocalStorage.SetValue("hp", hp);
        }
        public static int LoadHp()
        {
            return LocalStorage.GetValue("hp", Entity.MaxHp);
        }
        public static void SaveShapeTime(int shapeTime)
        {
            LocalStorage.SetValue("shapeTime", shapeTime);
        }
        public static int LoadShapeTime()
        {
            return LocalStorage.GetValue("shapeTime", ShapeController.ShapeTime);
        }

        public static void SaveSlimeName(string slimeName)
        {
            LocalStorage.SetValue("slimeName", slimeName);
        }
        public static string LoadSlimeName()
        {
            return LocalStorage.GetValue("slimeName", "");
        }

        public static void RemoveSlimeData()
        {
            LocalStorage.Remove("shapeTime");
            LocalStorage.Remove("hp");
            LocalStorage.Remove("speed");
            LocalStorage.Remove("money");
            LocalStorage.Remove("level");
            LocalStorage.Remove("experience");
        }
    }
}