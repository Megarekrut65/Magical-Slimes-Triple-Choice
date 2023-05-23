using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Game.Choice
{
    public class ChoiceTimer : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private IEnumerator _timeGo;
        private bool _stopped;

        private Action _timeOut;

        public void StartTimer(float time, Action timeOut)
        {
            _timeOut = timeOut;
            
            slider.maxValue = time;
            slider.value = time;

            _stopped = false;
            
            _timeGo = TimeGo();
            StartCoroutine(_timeGo);
        }

        public void StopTimer()
        {
            _stopped = true;
            if (_timeGo != null) StopCoroutine(_timeGo);
        }

        private IEnumerator TimeGo()
        {
            while (slider.value > 0)
            {
                yield return new WaitForSeconds(0.1f);
                slider.value-=0.1f;
            }

            if (!_stopped) _timeOut();
        }
    }
}