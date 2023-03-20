using UnityEngine;

namespace Global
{
    public class MusicManager : MonoBehaviour {
        [SerializeField]
        private AudioSource audioSource;
        private bool _playNext = false;
        public bool PlayNext {
            set => _playNext = value;
        }
        public static MusicManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }

            LoadManager();
            DontDestroyOnLoad(gameObject);
        }
        private void LoadManager() {
            Instance.Volume(LocalStorage.GetValue("music", 0.5f));
            if (_playNext) {
                Instance.audioSource.Stop();
            } else if (!Instance.audioSource.isPlaying) {
                Instance.audioSource.Play();
            }
        }
        private void Start() {
            gameObject.GetComponent<AudioSource>().Play();
        }
        public void Volume(float value) {
            Instance.audioSource.volume = value;
        }
    }
}