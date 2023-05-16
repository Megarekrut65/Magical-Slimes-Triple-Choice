using UnityEngine;

namespace Global.Sound
{
    public class MusicManager : MonoBehaviour {
        [SerializeField]
        private AudioSource audioSource;
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
        private void LoadManager() 
        {
            Instance.Volume(LocalStorage.GetValue("music", 0.1f));
            if (!Instance.audioSource.isPlaying) {
                Instance.audioSource.Play();
            }
        }

        public static void Stop()
        {
            if (Instance != null)
            {
                Instance.audioSource.Stop();
            }
        }
        public static void Play()
        {
            if (Instance != null && !Instance.audioSource.isPlaying)
            {
                Instance.audioSource.Play();
            }
        }
        private void Start() {
            gameObject.GetComponent<AudioSource>().Play();
        }
        public void Volume(float value) {
            Instance.audioSource.volume = value;
        }
        public static void VolumeSound(float value) {
            if(Instance == null) return;
            
            Instance.Volume(value);
        }
    }
}