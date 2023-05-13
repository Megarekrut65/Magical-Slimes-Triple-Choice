using Global;
using Global.Json;
using UnityEngine;

namespace Fighting.Game
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

        public static int LoadCups()
        {
            return LocalStorage.GetValue("cups", 0);
        }

        public static void SaveCups(int cups)
        {
            LocalStorage.SetValue("cups", cups);
        }

        public static string LoadCode()
        {
            return LocalStorage.GetValue("privateCode", "");
        }

        public static void SaveCode(string code)
        {
            LocalStorage.SetValue("privateCode", code);
        }

        public static void SaveUserInfo(string key, UserInfo info)
        {
            ItemData<UserInfo> data = new ItemData<UserInfo>{key="info", value = info};
            LocalStorage.SetValue("enemyInfo", JsonUtility.ToJson(data));
        }

        public static UserInfo LoadUserInfo(string key)
        {
            ItemData<UserInfo> list = JsonUtility.FromJson<ItemData<UserInfo>>(
                LoadUserInfoJson(key));

            return list.value;
        }

        private static string LoadUserInfoJson(string key)
        {
            return LocalStorage.GetValue(key, "{\"info\":null}");
        }
        public static void SaveMainType(string type)
        {
            LocalStorage.SetValue("mainType", type);
        }

        public static string LoadMainType()
        {
            return LocalStorage.GetValue("mainType", "host");
        }
    }
}