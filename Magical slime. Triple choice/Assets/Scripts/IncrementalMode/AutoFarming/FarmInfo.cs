using System;

namespace IncrementalMode.AutoFarming
{
    [Serializable]
    public class FarmInfo
    {
        public string title;
        public int level;
        public ulong price;
        public ulong startAmount;
        public int amountFunctionIndex;
    }
}