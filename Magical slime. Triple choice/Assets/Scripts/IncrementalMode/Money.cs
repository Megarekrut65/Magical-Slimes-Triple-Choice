public enum MoneyConstants : long
{
    None = 1,
    K = 1000,
    M = 1000000,
    B = 1000000000,
    T = 1000000000000
}

public class Money
{
    public long Amount { get; private set; }
    private MoneyConstants _moneyConstant;

    public Money(long amount)
    {
        Amount = amount;
        Converter();
    }

    private void Converter()
    {
        _moneyConstant = MoneyConstants.None;
        if (Amount > (long)MoneyConstants.K) _moneyConstant = MoneyConstants.K;
        else if (Amount > (long)MoneyConstants.M) _moneyConstant = MoneyConstants.M;
        else if (Amount > (long)MoneyConstants.B) _moneyConstant = MoneyConstants.B;
        else if (Amount > (long)MoneyConstants.T) _moneyConstant = MoneyConstants.T;
    }

    public void Add(long value)
    {
        Amount += value;
        Converter();
    }

    public override string ToString()
    {
        string constant = _moneyConstant == MoneyConstants.None ? " " : _moneyConstant.ToString();
        return $"{(double)Amount / (long)_moneyConstant:0.000}{constant}";
    }
}