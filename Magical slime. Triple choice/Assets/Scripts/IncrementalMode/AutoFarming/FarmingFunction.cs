using System;

namespace IncrementalMode.AutoFarming
{
    public static class FarmingFunction
    {
        public static Money Eval(int index, ulong startAmount, int level)
        {
            double value =  index switch
            {
                0=> startAmount * (ulong)level - Math.Log10(startAmount),
                1=>Math.Pow(startAmount, level) - Math.Exp(level),
                2=>startAmount * 10 + Math.Pow(level, 10) + level,
                3=>Math.Log10(startAmount) + Math.Exp(level),
            };

            return new Money(Math.Max(0, (ulong)value));
        }
    }
}