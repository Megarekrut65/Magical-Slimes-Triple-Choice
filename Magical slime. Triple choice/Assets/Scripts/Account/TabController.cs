using System;
using UnityEngine;
using UnityEngine.UI;

namespace Account
{
    public class TabController : MonoBehaviour
    {
        [SerializeField] private TabItem[] tabs;

        [SerializeField] private Color buttonOn;
        [SerializeField] private Color buttonOff;
        private void Start()
        {
            Click(0);
        }

        public void Click(int index)
        {
            foreach (TabItem tab in tabs)
            {
                if (index == tab.index)
                {
                    tab.place.SetActive(true);
                    tab.button.color = buttonOn;
                }
                else
                {
                    tab.place.SetActive(false);
                    tab.button.color = buttonOff;
                }
            }
        }
    }
}