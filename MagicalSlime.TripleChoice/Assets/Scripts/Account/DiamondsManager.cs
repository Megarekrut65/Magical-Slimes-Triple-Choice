using Global;
using UnityEngine;
using UnityEngine.UI;

namespace Account
{
    public class DiamondsManager : MonoBehaviour
    {
        [SerializeField] private Text diamondsText;

        private void Start()
        {
            diamondsText.text = DataSaver.LoadDiamonds().ToString();
        }

        public bool Buy(int price)
        {
            int diamonds = DataSaver.LoadDiamonds();
            if (price > diamonds) return false;
            int newDiamonds = diamonds - price;
            DataSaver.SaveDiamonds(newDiamonds);
            diamondsText.text = newDiamonds.ToString();
            
            return true;
        }
            
    }
}