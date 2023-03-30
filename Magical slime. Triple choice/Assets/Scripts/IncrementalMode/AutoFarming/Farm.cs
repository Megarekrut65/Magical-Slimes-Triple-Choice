using System;

namespace IncrementalMode.AutoFarming
{
    public class Farm
    {
        public FarmInfo Info { get; }

        public Money Amount => AmountFunction.Eval(Info.amountFunctionIndex, Info.startAmount, Info.level);
        public Money Price => PriceFunction.Eval(Info.priceFunctionIndex, Info.startPrice, Info.level);
        public Farm(FarmInfo farmInfo)
        {
            Info = farmInfo;
        }
        
    }
}