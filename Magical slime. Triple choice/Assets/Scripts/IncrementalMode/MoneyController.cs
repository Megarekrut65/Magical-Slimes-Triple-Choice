using IncrementalMode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private long clickAmount;
    private readonly Money _money = new Money(0);

    public void Click(float percent)
    {
        _money.Add((long)((percent + 1) * clickAmount));
        text.text = _money.ToString();
    }
}