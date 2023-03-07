using IncrementalMode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private long clickAmount;
    private readonly Money _money = new Money(0);

    public long Click(float percent)
    {
        long amount = (long)((percent + 1) * clickAmount);
        _money.Add(amount);
        text.text = _money.ToString();
        
        return amount;
    }
}