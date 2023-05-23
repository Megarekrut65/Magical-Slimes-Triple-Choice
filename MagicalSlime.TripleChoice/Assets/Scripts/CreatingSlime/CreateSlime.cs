using Global;
using Global.Localization;
using IncrementalMode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CreatingSlime
{
    /// <summary>
    /// Saves new lsime data from GUI from to local storage
    /// </summary>
    public class CreateSlime: MonoBehaviour
    {
        [SerializeField] private InputField nameInput;
        [SerializeField] private Text errorMessage;

        private bool _isClicked;

        public void Submit()
        {
            if(_isClicked) return;
            _isClicked = true;
            
            string slimeName = nameInput.text;
            if (slimeName.Length > 3)
            {
                DataSaver.SaveLevel(DataSaver.LoadLevel()/2);
                DataSaver.RemoveSlimeData();
                DataSaver.SaveSlimeName(slimeName);
                DataSaver.SaveShop("life", 1);

                SceneManager.LoadScene("IncrementalMode", LoadSceneMode.Single);
                return;
            }

            _isClicked = false;
            errorMessage.text = LocalizationManager.GetWordByKey("name-length");
        }
    }
}