using System;

namespace IncrementalMode.AutoFarming
{
    public static class AmountFunction
    {
        public static Money Eval(int index, ulong startAmount, int level)
        {
            double value =  index switch
            {
                0=> startAmount * (ulong)level + 1,
                1=> startAmount * (ulong)(level+1) + 1,
                2=> startAmount * (ulong)(level*2 + 1) + 100,
                _=>startAmount * (ulong)level
            };

            return new Money(Math.Max(0, (ulong)value));
        }
    }

    public static class PriceFunction
    {
        public static Money Eval(int index, ulong startPrice, int level)
        {
            double value =  index switch
            {
                0=> startPrice * (ulong)(level * 2 + 1) + Math.Log10(startPrice),
                1=> startPrice * (ulong)(level * 2 + 1),
                2=> startPrice * (ulong)(level * 3 + 1) - Math.Log10(startPrice),
                _=>startPrice * (ulong)level
            };

            return new Money(Math.Max(0, (ulong)value));
        }
    }
}