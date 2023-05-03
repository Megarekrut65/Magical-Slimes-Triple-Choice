using System;
using System.Collections.Generic;
using Firebase.Auth;
using DataBase;
using Global;
using LoginRegister;
using Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Account.UserInfo
{
    public class InfoManager : MonoBehaviour
    {
        [Header("Places")]
        [SerializeField] private GameObject infoBox;
        [SerializeField] private GameObject loginBox;
        
        [Header("Info place")]
        [SerializeField] private Text usernameText;
        [SerializeField] private Text registrationDateText;
        [SerializeField] private Text maxEnergyText;
        [SerializeField] private Text maxLevelText;

        private void Start()
        {
            bool active = LoginController.UserSignIn();
            loginBox.SetActive(!active);
            infoBox.SetActive(active);

            if (!active) return;
            usernameText.text = DataSaver.LoadUsername();
            registrationDateText.text = DataSaver.LoadRegistrationDate();
            maxEnergyText.text = DataSaver.LoadMaxEnergyForAccount().ToString();
            maxLevelText.text = DataSaver.LoadMaxLevelForAccount().ToString();
        }
        
        public void LogOut()
        {
            LoginController.SignOutUser();
            SceneManager.LoadScene("LoginRegister", LoadSceneMode.Single);
            DataSaver.RemoveAccountData();
        }
    }
}