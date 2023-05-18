
using System.Collections;
using Global;
using IncrementalMode;
using LoginRegister;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Account.UserInfo
{
    public class InfoManager : MonoBehaviour
    {
        [SerializeField] private ScreenLoader loader;
        
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
            LocalStorage.GetValue("needSave", "true");
            if (DataSaver.LoadSlimeName() == "")
            {
                SceneManager.LoadScene("SlimeCreating", LoadSceneMode.Single);
                return;
            }
            
            bool active = LoginController.UserSignIn();
            loginBox.SetActive(!active);
            infoBox.SetActive(active);

            if (!active) return;
            usernameText.text = DataSaver.LoadUsername();
            if (DataSaver.LoadUsername() == "")
            {
                Out();
                return;
            }
            registrationDateText.text = DataSaver.LoadRegistrationDate();
            maxEnergyText.text = new Energy(DataSaver.LoadMaxEnergyForAccount()).ToString();
            maxLevelText.text = DataSaver.LoadMaxLevelForAccount().ToString();
        }
        
        public void LogOut()
        {
            LocalStorage.SetValue("needSave", "false");
            loader.Show();
            LoginController.SignOutUser();
            DataSaver.RemoveAccountData();
            StartCoroutine(Open());
        }

        public void Out()
        {
            LocalStorage.SetValue("needSave", "false");
            LoginController.SignOutUser();
            StartCoroutine(Open());
        }
        private IEnumerator Open()
        {
            yield return new WaitForSeconds(0.1f);
            loader.Hide();
            SceneManager.LoadScene("LoginRegister", LoadSceneMode.Single);
        }
    }
}