using System;

namespace IncrementalMode.AutoFarming
{
    /// <summary>
    /// Functions of changing of auto farms profit depending on auto farm level. 
    /// </summary>
    public static class ProfitFunction
    {
        public static Energy Eval(int index, ulong startProfit, int level)
        {
            double value =  index switch
            {
                0=> startProfit * (ulong)level + 1,
                1=> startProfit * (ulong)(level+1) + 1,
                2=> startProfit * (ulong)(level*2 + 1) + 100,
                _=>startProfit * (ulong)level
            };

            return new Energy(Math.Max(0, (ulong)(value * Math.Sign(level))));
        }
    }
    /// <summary>
    /// Functions of changing of auto farms price depending on auto farm level. 
    /// </summary>
    public static class PriceFunction
    {
        public static Energy Eval(int index, ulong startPrice, int level)
        {
            double value =  index switch
            {
                0=> startPrice * (ulong)(level * 2 + 1) + Math.Log10(startPrice),
                1=> startPrice * (ulong)(level * 2 + 1),
                2=> startPrice * (ulong)(level * 3 + 1) - Math.Log10(startPrice),
                _=>startPrice * (ulong)level
            };

            return new Energy(Math.Max(0, (ulong)value));
        }
    }
}