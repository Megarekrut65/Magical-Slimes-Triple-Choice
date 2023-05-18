using System.Collections.Generic;
using DataManagement;
using Firebase.Auth;
using Global.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LoginRegister
{
    /// <summary>
    /// Manages conflicts of user data after logining.
    /// </summary>
    public class ConflictManager : MonoBehaviour
    {
        [SerializeField] private Text errorText;
        [SerializeField] private GameObject board;
        
        [SerializeField] private InfoLoader account;
        [SerializeField] private InfoLoader device;

        private Info _accountInfo;
        private Info _deviceInfo;

        private Dictionary<string, object> _accountData;
        private Dictionary<string, object> _deviceData;
        
        public void Conflict(Dictionary<string, object> accountData, Dictionary<string, object> deviceData)
        {
            board.SetActive(true);
            _accountInfo = DictionaryToInfo.Get(accountData);
            _deviceInfo = DictionaryToInfo.Get(deviceData);
            _accountData = accountData;
            _deviceData = deviceData;
            
            account.Load(_accountInfo);
            device.Load(_deviceInfo);
        }

        private void Load(Dictionary<string, object> data)
        {
            DatabaseLoader loader = new DatabaseLoader();
            loader.LoadData(data);
            DatabaseSaver saver = new DatabaseSaver();
            FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

            saver.SaveUserData(user.UserId, (success, message) =>
            {
                if (success)
                {
                    SceneManager.LoadScene("Account", LoadSceneMode.Single);
                    return;
                }

                errorText.text = LocalizationManager.GetWordByKey(message);
            });
        }
        public void ChooseDevice()
        {
            Load(_deviceData);
        }

        public void ChooseAccount()
        {
            Load(_accountData);
        }

        public void Merge()
        {
            Load(Merger.Merge(_accountData, _deviceData));
        }
    }
}