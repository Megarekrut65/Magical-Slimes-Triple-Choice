using System.Collections.Generic;
using UnityEngine;

namespace Global.Localization
{
    public static class JsonParser<TValueType> {
        public static SortedDictionary<string, TValueType> Parse(string valuePath) {
            string path = Application.streamingAssetsPath + "/" + valuePath + ".json";
            string jsonData = AllFileReader.Read(path);
            LocalizationList<TValueType> list = JsonUtility.FromJson<LocalizationList<TValueType>>(jsonData);
            var map = new SortedDictionary<string, TValueType>();
            if (list.items != null) {
                foreach (var item in list.items) {
                    map.Add(item.key, item.value);
                }
            }

            return map;
        }
    }
}