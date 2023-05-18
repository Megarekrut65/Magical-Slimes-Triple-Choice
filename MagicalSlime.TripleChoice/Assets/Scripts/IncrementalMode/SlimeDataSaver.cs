using System;
using Account.SlimesList;
using Global;

namespace IncrementalMode
{
    /// <summary>
    /// Saves current slime data from local storage to slimes list in local storage.
    /// </summary>
    public static class SlimeDataSaver
    {
        public static void SaveCurrentSlimeResult()
        {
            SlimeData slimeData = new SlimeData
            {
                energy = new Energy(DataSaver.LoadMaxEnergy()).ToString(),
                level = DataSaver.LoadLevel(),
                name = DataSaver.LoadSlimeName(),
                key = DataSaver.LoadSlimeType()
            };

            SlimeData[] data = DataSaver.LoadSlimeData() ?? new SlimeData[] { };
            SlimeData find = Array.Find(data, item =>item.Equals(slimeData));

            if(find != null) return;
            
            SlimeData[] newData = new SlimeData[data.Length + 1];
            data.CopyTo(newData, 0);
            newData[data.Length] = slimeData;
            
            DataSaver.SaveSlimeData(newData);
        }
    }
}