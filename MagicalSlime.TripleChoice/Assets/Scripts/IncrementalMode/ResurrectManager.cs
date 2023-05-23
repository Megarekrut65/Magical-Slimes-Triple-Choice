using System;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IncrementalMode
{
    /// <summary>
    /// Controls entity resurrecting after death.
    /// </summary>
    public class ResurrectManager : MonoBehaviour
    {
        [SerializeField] private Entity entity;
        
        [SerializeField] private GameObject place;
        [SerializeField] private Text amountText;
        
        [SerializeField] private Text diamondsText;

        [SerializeField] private int resurrectAmount;
        private int _diamonds;

        private void Start()
        {
            amountText.text = resurrectAmount.ToString();
        }

        public void EntityDie()
        {
            _diamonds = DataSaver.LoadDiamonds();
            if(_diamonds >= resurrectAmount) Show();
            else Leave();
        }
        private void Show()
        {
            place.SetActive(true);
        }

        public void Resurrect()
        {
            DataSaver.SaveDiamonds(_diamonds - resurrectAmount);
            diamondsText.text = DataSaver.LoadDiamonds().ToString();
            place.SetActive(false);
            
            entity.Resurrect();
        }

        public void Leave()
        {
            SlimeDataSaver.SaveCurrentSlimeResult();
            SceneManager.LoadScene("SlimeCreating", LoadSceneMode.Single);
        }
    }
}