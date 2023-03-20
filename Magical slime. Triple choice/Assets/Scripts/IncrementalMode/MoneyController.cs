using System;
using Global;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncrementalMode
{
    public class MoneyController : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private int clickAmount;
        private readonly Money _money = new Money(0);

        private void Start()
        {
            _money.Add(DataSaver.LoadMoney());
            text.text = _money.ToString();
        }

        public ulong Click(float percent, int level)
        {
            ulong amount = (ulong)(Math.Log(level+5)*(percent + 1) * clickAmount 
                                   + level + 1 + Random.Range(0, level*clickAmount));
            _money.Add(amount);
        
            DataSaver.SaveMoney(_money.Amount);
        
            text.text = _money.ToString();
        
            return amount;
        }
    }
}