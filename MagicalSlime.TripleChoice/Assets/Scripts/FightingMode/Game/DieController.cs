﻿using System.Collections;
using FightingMode.Game.EntityControllers;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FightingMode.Game
{
    public class DieController : MonoBehaviour
    {
        [SerializeField] private HealthController mainHealthController;
        [SerializeField] private HealthController enemyHealthController;

        private int _losersCount = 0;
        private string _lastWinner = "";
        public bool IsGameOver()
        {
            return mainHealthController.IsDied || enemyHealthController.IsDied;
        }

        private void LoseEvent(HealthController healthController, string type)
        {
            if(!healthController.IsDied) return;
            healthController.Die();
            _lastWinner = type;
            _losersCount++;
        }
        public void GameOver()
        {
            LoseEvent(mainHealthController, "mainInfo");
            LoseEvent(enemyHealthController, "enemyInfo");

            GameOverSaver saver = new GameOverSaver(FightingSaver.LoadUserInfo("mainInfo"), 
                FightingSaver.LoadUserInfo("enemyInfo"));

            string mainType = FightingSaver.LoadMainType(), roomType = FightingSaver.LoadRoomType();
            if (_losersCount == 1)
            {
                saver.Save(_lastWinner, mainType, roomType);
            }
            else
            {
                saver.SaveDraw(mainType, roomType);
            }
            
            CustomLogger.Log(JsonUtility.ToJson(FightingSaver.LoadUserInfo("winner")));
            CustomLogger.Log(JsonUtility.ToJson(FightingSaver.LoadUserInfo("loser")));
            CustomLogger.Log("GameOver!");
            StartCoroutine(WaitForLoad());
        }

        private IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }
}