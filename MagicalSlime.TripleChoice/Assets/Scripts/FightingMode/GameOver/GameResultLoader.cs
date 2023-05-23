using System;
using DataManagement;
using FightingMode.Game;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.GameOver
{
    public class GameResultLoader : MonoBehaviour
    {
        [SerializeField] private Color addColor;
        [SerializeField] private Color minusColor;
        
        [SerializeField] private Text cupsText;
        [SerializeField] private Text diamondsText;

        private void Start()
        {
            GameResult result = FightingSaver.LoadResult();

            SetValue(cupsText, result.deltaCups);
            SetValue(diamondsText, result.deltaDiamonds);
            
            DataSaver.SaveDiamonds(DataSaver.LoadDiamonds() + result.deltaDiamonds);
            FightingSaver.SaveCups(FightingSaver.LoadCups() + result.deltaCups);
            FightingSaver.SaveGameOver(true);
            DataSync sync = new DataSync();
            sync.SyncAllData((_,_)=>{});
        }

        private void SetValue(Text text, int value)
        {
            text.text = (value > 0?"+":"") + value;
            text.color = value < 0 ? minusColor : addColor;
        }
    }
}