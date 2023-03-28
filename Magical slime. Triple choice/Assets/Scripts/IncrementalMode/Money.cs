namespace IncrementalMode
{
    public enum MoneyConstants : ulong
    {
        None = 1,
        K = 1000,
        M = 1000000,
        B = 1000000000,
        T = 1000000000000,
        Qd = 1000000000000000,
        Qr = 1000000000000000000
    }

    public class Money
    {
        public ulong Amount { get; private set; }
        private MoneyConstants _moneyConstant;

        public Money(ulong amount)
        {
            Amount = amount;
            Converter();
        }

        private void Converter()
        {
            _moneyConstant = MoneyConstants.None;
            if (Amount >= (ulong)MoneyConstants.K) _moneyConstant = MoneyConstants.K;
            if (Amount >= (ulong)MoneyConstants.M) _moneyConstant = MoneyConstants.M;
            if (Amount >= (ulong)MoneyConstants.B) _moneyConstant = MoneyConstants.B;
            if (Amount >= (ulong)MoneyConstants.T) _moneyConstant = MoneyConstants.T;
            if (Amount >= (ulong)MoneyConstants.Qd) _moneyConstant = MoneyConstants.Qd;
            if (Amount >= (ulong)MoneyConstants.Qr) _moneyConstant = MoneyConstants.Qr;
        }

        public void Add(ulong value)
        {
            Amount += value;
            Converter();
        }

        public void Remove(ulong value)
        {
            if (Amount >= value) Amount -= value;
        }
        public override string ToString()
        {
            double amount = (double)Amount / (ulong)_moneyConstant;
            return _moneyConstant == MoneyConstants.None ? $"{amount}" : $"{amount:0.000}{_moneyConstant}";
        }
    }
}