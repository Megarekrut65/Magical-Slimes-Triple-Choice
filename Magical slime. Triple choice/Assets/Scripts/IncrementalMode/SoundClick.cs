using Global.Sound;
using UnityEngine;

namespace IncrementalMode
{
    public class SoundClick : MonoBehaviour
    {
        public void Click()
        {
            if(!SoundManager.Instance.IsPlaying(2)) SoundManager.Instance.Play(2);
        }
    }
}