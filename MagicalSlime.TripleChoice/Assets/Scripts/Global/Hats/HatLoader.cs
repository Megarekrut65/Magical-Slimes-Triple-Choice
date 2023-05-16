using System;
using UnityEngine;

namespace Global.Hats
{
    public class HatLoader : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer currentHat;

        private void Start()
        {
            Hat hat = HatsList.GetCurrentHat();
            if(hat == null) return;
            currentHat.sprite = hat.icon;
        }
    }
}