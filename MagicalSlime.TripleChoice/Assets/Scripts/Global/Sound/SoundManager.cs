using UnityEngine;

namespace Global.Sound
{
    /// <summary>
    /// Manages sound volume changing and saving.
    /// </summary>
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
        public static void VolumeSound(float value) {
            if(Instance == null) return;
            
            Instance.Volume(value);
        }
        public static void PlaySound(int index)
        {
            if(Instance == null) return;
            
            Instance.Play(index);
        }

        public static bool IsSoundPlaying(int index)
        {
            return Instance != null && Instance.IsPlaying(index);
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