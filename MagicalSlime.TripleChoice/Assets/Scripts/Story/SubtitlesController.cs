using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class SubtitlesController : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Animation anim;

        private string _newSubtitles;
        
        public void SetSubtitles(string subtitles)
        {
            _newSubtitles = subtitles;
            anim.Play();
        }

        private void Change()
        {
            text.text = _newSubtitles;
        }
    }
}