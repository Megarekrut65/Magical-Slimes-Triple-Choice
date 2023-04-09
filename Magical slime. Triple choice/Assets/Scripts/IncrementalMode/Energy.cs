using System;
using System.Numerics;

namespace IncrementalMode
{
    public enum EnergyConstants : ulong
    {
        None = 1,
        K = 1000,
        M = 1000000,
        B = 1000000000,
        T = 1000000000000,
        Qd = 1000000000000000,
        Qr = 1000000000000000000
    }

    public class Energy
    {
        public BigInteger Amount { get; private set; }
        private EnergyConstants _energyConstant;

        public Energy(BigInteger amount)
        {
            Amount = amount;
            Converter();
        }

        private void Converter()
        {
            _energyConstant = EnergyConstants.None;
            if (Amount >= (ulong)EnergyConstants.K) _energyConstant = EnergyConstants.K;
            if (Amount >= (ulong)EnergyConstants.M) _energyConstant = EnergyConstants.M;
            if (Amount >= (ulong)EnergyConstants.B) _energyConstant = EnergyConstants.B;
            if (Amount >= (ulong)EnergyConstants.T) _energyConstant = EnergyConstants.T;
            if (Amount >= (ulong)EnergyConstants.Qd) _energyConstant = EnergyConstants.Qd;
            if (Amount >= (ulong)EnergyConstants.Qr) _energyConstant = EnergyConstants.Qr;
        }

        public void Add(BigInteger value)
        {
            Amount += value;
            Converter();
        }

        public void Remove(BigInteger value)
        {
            if (Amount >= value) Amount -= value;
        }
        public override string ToString()
        {
            BigInteger amount = Amount / (ulong)_energyConstant;
            string part = (Amount % (ulong)_energyConstant).ToString();
            while (part.Length < 3) part += "0";
            
            return _energyConstant == EnergyConstants.None 
                ? $"{amount}" 
                : $"{amount}.{part.Substring(0,3)}" +
                  $"{_energyConstant}".Replace(',','.');
        }
    }
}