using System.Collections;
using Global.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Global.Localization
{
    /// <summary>
    /// Controls language selecting.
    /// </summary>
    public class LocalizationButton : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler {
        [SerializeField]
        private string language = "";
        [SerializeField]
        private LanguageLoader languageLoader;
        [SerializeField]
        private Outline outline;
        
        public void OnPointerDown(PointerEventData eventData) {
            languageLoader.SetActive(true);
            SoundManager.PlaySound(1);
            LocalizationManager.Instance.ChangeLanguage(language);
        }
        public void OnPointerUp(PointerEventData eventData) {
        }
        private void OnDestroy() {
            LocalizationManager.Instance.OnLanguageChanged -= ChangeLanguage;
        }
        private void Awake() {
            LocalizationManager.Instance.OnLanguageChanged += ChangeLanguage;
        }
        private void Start() {
            ChangeLanguage();
        }

        private void ChangeLanguage() {
            var alpha = 0f;
            if (PlayerPrefs.HasKey("Language") &&
                PlayerPrefs.GetString("Language").Equals(language)) {
                alpha = 0.5f;
                StartCoroutine(Hide());
            }

            outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, alpha);
        }
        private IEnumerator Hide() {
            yield return new WaitForSeconds(0.5f);
            languageLoader.SetActive(false);
        }
    }
}