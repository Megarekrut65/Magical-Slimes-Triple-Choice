using System;

namespace IncrementalMode.AutoFarming
{
    public class Farm
    {
        public FarmInfo Info { get; private set; }

        public Money Amount => FarmingFunction.Eval(Info.amountFunctionIndex, Info.startAmount, Info.level);
        public Farm(FarmInfo farmInfo)
        {
            Info = farmInfo;
        }
        
    }
}