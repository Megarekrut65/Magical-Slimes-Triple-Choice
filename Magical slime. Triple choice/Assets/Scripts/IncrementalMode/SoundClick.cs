using UnityEngine;

namespace IncrementalMode
{
    public class SoundClick : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void Click()
        {
            if(!audioSource.isPlaying) audioSource.Play();
        }
    }
}