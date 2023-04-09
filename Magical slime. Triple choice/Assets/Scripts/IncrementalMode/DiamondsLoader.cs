using System;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    public class DiamondsLoader : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Start()
        {
            text.text = DataSaver.LoadDiamonds().ToString();
        }
    }
}