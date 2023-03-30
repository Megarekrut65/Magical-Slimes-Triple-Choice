using System.Collections;
using Global;
using UnityEngine;

namespace IncrementalMode.Shop
{
    public abstract class TimeShopItem : ActiveShopItem
    {
        [Header("Time item")]
        [SerializeField] private int increaseTime;

        [SerializeField] private string key;
        
        private bool _isActive = false;

        protected abstract void OnTimeBegin();
        protected abstract void OnTimeEnd();
        
        protected override void OnStart()
        {
            base.OnStart();
            StartCoroutine(TimeGo());

            Entity.OnEntityDied += Die;
        }

        private void Die()
        {
            LocalStorage.Remove(key);
        }
        
        protected void StartTime()
        {
            int time = LocalStorage.GetValue(key, 0);
            LocalStorage.SetValue(key, time + increaseTime);
            
            if(_isActive) return;
            
            StartCoroutine(TimeGo());
        }
        private IEnumerator TimeGo()
        {
            _isActive = true;
            ActiveOn();

            int time = LocalStorage.GetValue(key, 0);
            OnTimeBegin();
            
            while (time > 0)
            {
                yield return new WaitForSeconds(1f);
                time = LocalStorage.GetValue(key, 0);
                LocalStorage.SetValue(key, time - 1);
            }
            
            OnTimeEnd();
            _isActive = false;
            ActiveOff();
        }
    }
}