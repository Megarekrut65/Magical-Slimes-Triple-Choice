using System;

namespace IncrementalMode.AutoFarming
{
    /// <summary>
    /// Gets actual price and profit from farm info.
    /// </summary>
    public class Farm
    {
        public FarmInfo Info { get; }

        public Energy Profit => ProfitFunction.Eval(Info.amountFunctionIndex, Info.startAmount, Info.level);
        public Energy Price => PriceFunction.Eval(Info.priceFunctionIndex, Info.startPrice, Info.level);
        public Farm(FarmInfo farmInfo)
        {
            Info = farmInfo;
        }
        
    }
}