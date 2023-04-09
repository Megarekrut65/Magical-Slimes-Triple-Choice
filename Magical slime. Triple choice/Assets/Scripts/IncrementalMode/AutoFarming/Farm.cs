using System;

namespace IncrementalMode.AutoFarming
{
    public class Farm
    {
        public FarmInfo Info { get; }

        public Energy Amount => AmountFunction.Eval(Info.amountFunctionIndex, Info.startAmount, Info.level);
        public Energy Price => PriceFunction.Eval(Info.priceFunctionIndex, Info.startPrice, Info.level);
        public Farm(FarmInfo farmInfo)
        {
            Info = farmInfo;
        }
        
    }
}