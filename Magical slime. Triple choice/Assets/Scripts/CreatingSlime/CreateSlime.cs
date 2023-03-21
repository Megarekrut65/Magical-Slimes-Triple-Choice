using Global;
using IncrementalMode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CreatingSlime
{
    public class CreateSlime: MonoBehaviour
    {
        [SerializeField] private InputField nameInput;
        [SerializeField] private Text errorMessage;

        public void Submit()
        {
            string slimeName = nameInput.text;
            if (slimeName.Length > 3)
            {
                DataSaver.RemoveSlimeData();
                DataSaver.SaveSlimeName(slimeName);

                SceneManager.LoadScene("IncrementalMode", LoadSceneMode.Single);
                return;
            }

            errorMessage.text = "Name length must be greater than 3";
        }
    }
}