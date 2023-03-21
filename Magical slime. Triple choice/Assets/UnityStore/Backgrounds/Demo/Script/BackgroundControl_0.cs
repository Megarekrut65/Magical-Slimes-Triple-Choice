using UnityEngine;
using UnityEngine.Serialization;

namespace UnityStore.Backgrounds.Demo.Script
{
    public class BackgroundControl0 : MonoBehaviour
    {
        [Header("BackgroundNum 0 -> 3")] public int backgroundNum;

        [FormerlySerializedAs("Layer_Sprites")]
        public Sprite[] layerSprites;

        private GameObject[] _layerObject = new GameObject[5];
        private int _maxBackgroundNum = 3;

        void Start()
        {
            for (int i = 0; i < _layerObject.Length; i++)
            {
                _layerObject[i] = GameObject.Find("Layer_" + i);
            }

            ChangeSprite();
        }

        void Update()
        {
            //for presentation without UIs
            if (Input.GetKeyDown(KeyCode.RightArrow)) NextBg();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) BackBg();
        }

        void ChangeSprite()
        {
            _layerObject[0].GetComponent<SpriteRenderer>().sprite = layerSprites[backgroundNum * 5];
            for (int i = 1; i < _layerObject.Length; i++)
            {
                Sprite changeSprite = layerSprites[backgroundNum * 5 + i];
                //Change Layer_1->7
                _layerObject[i].GetComponent<SpriteRenderer>().sprite = changeSprite;
                //Change "Layer_(*)x" sprites in children of Layer_1->7
                _layerObject[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = changeSprite;
                _layerObject[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = changeSprite;
            }
        }

        public void NextBg()
        {
            backgroundNum = backgroundNum + 1;
            if (backgroundNum > _maxBackgroundNum) backgroundNum = 0;
            ChangeSprite();
        }

        public void BackBg()
        {
            backgroundNum = backgroundNum - 1;
            if (backgroundNum < 0) backgroundNum = _maxBackgroundNum;
            ChangeSprite();
        }
    }
}