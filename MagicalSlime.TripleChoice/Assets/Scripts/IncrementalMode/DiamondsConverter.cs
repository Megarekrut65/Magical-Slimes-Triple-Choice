using System;
using System.Numerics;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class DiamondsConverter: MonoBehaviour
    {
        [Header("Fields with energy and diamonds")]
        [SerializeField] private Text diamondsText;
        [SerializeField] private EnergyController energyController;
        [Header("Fields for converting")]
        [SerializeField] private Text diamondsAmount;
        [SerializeField] private Text energyAmount;

        [Header("Convert amount")] 
        [SerializeField] private int energyForDiamond;
        [SerializeField] private int minimumAmount;

        private void Start()
        {
            diamondsAmount.text = "10";
            energyAmount.text = ""+energyForDiamond * minimumAmount;
        }

        public void Convert()
        {
            int diamonds = DataSaver.LoadDiamonds();
            if (diamonds < minimumAmount) return;
            
            diamonds -= minimumAmount;
            diamondsText.text = diamonds.ToString();
            DataSaver.SaveDiamonds(diamonds);
                
            energyController.AddMoney(new Energy(new BigInteger(minimumAmount*energyForDiamond)));
        }
    }
}