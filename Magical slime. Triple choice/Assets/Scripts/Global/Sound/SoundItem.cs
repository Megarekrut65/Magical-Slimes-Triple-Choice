using UnityEngine;

namespace Global.Sound
{
    public class SoundItem : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private float volumeDelta;

        private float _volume;
        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value + volumeDelta;
                source.volume = _volume;
            }
        }

        public bool IsPlaying => source.isPlaying;
        public void Play()
        {
            source.Play();
        }
    }
}