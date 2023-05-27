using System;
using Global;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IncrementalMode
{
    /// <summary>
    /// Controls current energy. Adding new energy by clicking or auto farm profit.
    /// Removing energy after new things buying.
    /// </summary>
    public class EnergyController : MonoBehaviour
    {
        public delegate void EnergyChanged(Energy energy);

        public static event EnergyChanged OnMoneyChanged;
        
        [SerializeField] private Text text;
        [SerializeField] private int clickAmount;
        
        public readonly Energy energy = new Energy(0);

        private void Start()
        {
            energy.Add(DataSaver.LoadEnergy());
            text.text = energy.ToString();
        }

        private void ChangeMoney()
        {
            DataSaver.SaveEnergy(energy.Amount);
            
            OnMoneyChanged?.Invoke(energy);
        
            text.text = energy.ToString();
        }
        public void AddMoney(Energy amount)
        {
            energy.Add(amount.Amount);

            ChangeMoney();
        }
        public ulong Click(float percent, int level)
        {
            ulong amount = (ulong)(Math.Log(level*5 + 1)*(percent * 2) * clickAmount 
                                   + level + 1 + Random.Range(0, level*clickAmount));
            energy.Add(amount);

            ChangeMoney();
        
            return amount;
        }

        public bool Buy(Energy price)
        {
            if (price.Amount <= energy.Amount)
            {
                energy.Remove(price.Amount);
                ChangeMoney();
                return true;
            }

            return false;
        }


    }
}