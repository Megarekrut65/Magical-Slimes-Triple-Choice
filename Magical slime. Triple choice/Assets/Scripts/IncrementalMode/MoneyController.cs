using Global;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class MoneyController : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private ulong clickAmount;
        private readonly Money _money = new Money(0);

        private void Start()
        {
            _money.Add(DataSaver.LoadMoney());
            text.text = _money.ToString();
        }

        public ulong Click(float percent)
        {
            ulong amount = (ulong)((percent + 1) * clickAmount);
            _money.Add(amount);
        
            DataSaver.SaveMoney(_money.Amount);
        
            text.text = _money.ToString();
        
            return amount;
        }
    }
}