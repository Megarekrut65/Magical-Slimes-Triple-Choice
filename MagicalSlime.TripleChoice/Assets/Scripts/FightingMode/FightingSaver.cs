using System;
using FightingMode.Game;
using FightingMode.Game.Choice;
using Global;
using Global.Json;
using UnityEngine;

namespace FightingMode
{
    public static class FightingSaver
    {
        public static void SaveGameOver(bool value)
        {
            LocalStorage.SetValue("gameOver", value.ToString());
        }

        public static bool LoadGameOver()
        {
            return Convert.ToBoolean(LocalStorage.GetValue("gameOver", "true"));
        }
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
            return LocalStorage.GetValue("cups", 200);
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
            LocalStorage.SetValue(key, JsonUtility.ToJson(data));
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
            LocalStorage.SetValue("enemyType", type=="host"?"client":"host");
        }

        public static string LoadMainType()
        {
            return LocalStorage.GetValue("mainType", "host");
        }
        public static string LoadEnemyType()
        {
            return LocalStorage.GetValue("enemyType", "client");
        }

        public static void SaveRoomType(string type)
        {
            LocalStorage.SetValue("roomType", type);
        }

        public static string LoadRoomType()
        {
            return LocalStorage.GetValue("roomType", "private-rooms");
        }

        public static ChoiceType LoadDefaultChoice(string key)
        {
            return (ChoiceType)LocalStorage.GetValue(key+"DefaultChoice", 0);
        }
        public static void SaveDefaultChoice(string key, ChoiceType type)
        {
            LocalStorage.SetValue(key+"DefaultChoice", (int)type);
        }

        public static string LoadFirst()
        {
            return LocalStorage.GetValue("first", "host");
        }
        public static void SaveFirst(string type)
        {
            LocalStorage.SetValue("first", type);
        }

        public static void SaveResult(GameResult result)
        {
            ItemData<GameResult> data = new ItemData<GameResult>{key="result", value = result};
            LocalStorage.SetValue("result", JsonUtility.ToJson(data));
        }

        public static GameResult LoadResult()
        {
            ItemData<GameResult> list = JsonUtility.FromJson<ItemData<GameResult>>(
                LoadUserInfoJson("result"));

            return list.value;
        }
    }
}