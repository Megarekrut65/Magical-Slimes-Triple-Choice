using UnityEngine;
using UnityEngine.UI;

namespace Global.Localization
{
    /// <summary>
    /// Loads translated word by key from Localisation manager.
    /// </summary>
    public class LocalizationText : MonoBehaviour {
        [SerializeField]
        private string key;
        private Text _text;

        private void Awake() {
            if (_text == null) {
                _text = GetComponent<Text>();
            }

            LocalizationManager.Instance.OnLanguageChanged += UpdateText;
        }
        private void Start() {
            UpdateText();
        }

        private void OnDestroy() {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
        }

        private void UpdateText() {
            if (gameObject == null) return;
            if (_text == null) {
                _text = GetComponent<Text>();
            }

            _text.text = LocalizationManager.GetWordByKey(key);
        }
    }
}