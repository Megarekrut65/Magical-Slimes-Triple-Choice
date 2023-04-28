using System;
using System.Collections;
using Global.Localization;
using Global.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Story
{
    public class StoryController : MonoBehaviour
    {
        [SerializeField] private SubtitlesController subtitlesController;
        
        [SerializeField] private StoryInfo[] items;
        [SerializeField] private GameObject backgroundObject;
        [SerializeField] private Transform parentTransform;
        
        private StoryItem[] _storyItems;
        private GameObject[] _objects;

        private void Start()
        {
            MusicManager.Stop();
            
            _storyItems = new StoryItem[items.Length];
            _objects = new GameObject[items.Length];
            
            for(int i = 0; i < items.Length; i++)
            {
                GameObject obj = Instantiate(backgroundObject, parentTransform, false);
                obj.transform.SetAsFirstSibling();
                StoryItem storyItem = obj.GetComponent<StoryItem>();
                storyItem.SetInfo(items[i], i, PlayNext);
                
                _storyItems[i] = storyItem;
                _objects[i] = obj;
            }

            StartCoroutine(Play());
        }

        private IEnumerator Play()
        {
            yield return new WaitForSeconds(2f);
            _storyItems[0].PlayFrame();
            subtitlesController.SetSubtitles(LocalizationManager.GetWordByKey(items[0].key));
        } 
        private IEnumerator End()
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene("IncrementalMode", LoadSceneMode.Single);
        } 
        private void PlayNext(int index)
        {
            if (index >= _storyItems.Length)
            {
                StartCoroutine(End());
                return;
            }
            
            _storyItems[index].PlayFrame();
            subtitlesController.SetSubtitles(LocalizationManager.GetWordByKey(items[index].key));
        }
    }
}