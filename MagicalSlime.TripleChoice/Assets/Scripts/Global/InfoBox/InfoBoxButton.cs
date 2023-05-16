using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Global.InfoBox
{
    public class InfoBoxButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action Click { private get; set; }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Click();
        }
    }
}