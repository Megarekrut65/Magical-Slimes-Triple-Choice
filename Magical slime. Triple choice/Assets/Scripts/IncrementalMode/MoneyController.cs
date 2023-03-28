using System;
using Global;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncrementalMode
{
    public class MoneyController : MonoBehaviour
    {
        public delegate void MoneyChanged(Money money);

        public static event MoneyChanged OnMoneyChanged;
        
        [SerializeField] private Text text;
        [SerializeField] private int clickAmount;
        
        public readonly Money money = new Money(0);

        private void Start()
        {
            money.Add(DataSaver.LoadMoney());
            text.text = money.ToString();
        }

        private void ChangeMoney()
        {
            DataSaver.SaveMoney(money.Amount);
            
            OnMoneyChanged?.Invoke(money);
        
            text.text = money.ToString();
        }
        public void AddMoney(Money amount)
        {
            money.Add(amount.Amount);

            ChangeMoney();
        }
        public ulong Click(float percent, int level)
        {
            ulong amount = (ulong)(Math.Log(level+5)*(percent + 1) * clickAmount 
                                   + level + 1 + Random.Range(0, level*clickAmount));
            money.Add(amount);

            ChangeMoney();
        
            return amount;
        }

        public bool Buy(Money price)
        {
            if (price.Amount <= money.Amount)
            {
                money.Remove(price.Amount);
                ChangeMoney();
                return true;
            }

            return false;
        }
    }
}