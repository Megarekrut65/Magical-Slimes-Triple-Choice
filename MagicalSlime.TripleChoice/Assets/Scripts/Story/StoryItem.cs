using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Story
{
    public class StoryItem : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Animation anim;
        [SerializeField] private AudioSource audioSource;

        private int _index;
        private Action<int> _next;
        public void SetInfo(StoryInfo info, int index, Action<int> next)
        {
            background.sprite = info.background;
            audioSource.clip = info.clip;
            _index = index;
            _next = next;
        }

        public void PlayFrame()
        {
            StartCoroutine(Play());
        }
        private IEnumerator Play()
        {
            audioSource.PlayDelayed(0.2f);
            while (audioSource.isPlaying)
            {
                yield return new WaitForSeconds(0.1f);
            }
            anim.Play();
            _next(_index + 1);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}