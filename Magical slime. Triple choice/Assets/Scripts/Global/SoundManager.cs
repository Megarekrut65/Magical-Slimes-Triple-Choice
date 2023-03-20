using UnityEngine;

namespace Global
{
    public class SoundManager : MonoBehaviour {
        [SerializeField]
        private AudioSource[] sources;

        public static SoundManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }

            Volume(LocalStorage.GetValue("sound", 0.5f));
            DontDestroyOnLoad(gameObject);
        }
        public void Volume(float value) {
            foreach (var source in Instance.sources) {
                source.volume = value;
            }
        }
        public void Play(int index) {
            if (Instance.sources.Length > index && index >= 0) {
                Instance.sources[index].Play();
            }
        }
    }
}