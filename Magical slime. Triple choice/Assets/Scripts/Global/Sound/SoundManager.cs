using UnityEngine;

namespace Global.Sound
{
    public class SoundManager : MonoBehaviour {
        [SerializeField]
        private SoundItem[] sources;

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
            foreach (var source in sources) {
                source.Volume = value;
            }
        }
        public void Play(int index) {
            if (sources.Length > index && index >= 0) {
                sources[index].Play();
            }
        }

        public bool IsPlaying(int index)
        {
            if (sources.Length > index && index >= 0) {
                return sources[index].IsPlaying;
            }

            return false;
        }
    }
}