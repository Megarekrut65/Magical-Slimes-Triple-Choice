using Global.Sound;
using UnityEngine;

namespace IncrementalMode
{
    public class SoundClick : MonoBehaviour
    {
        public void Click()
        {
            if(!SoundManager.IsSoundPlaying(2)) SoundManager.PlaySound(2);
        }
    }
}