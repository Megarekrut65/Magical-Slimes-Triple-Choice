using System;

namespace Global.Json
{
    [Serializable]
    public class JsonList<TValueType> {
        public ItemData<TValueType>[] items;
    }

    [Serializable]
    public class ItemData<TValueType> {
        public string key;
        public TValueType value;
    }
}