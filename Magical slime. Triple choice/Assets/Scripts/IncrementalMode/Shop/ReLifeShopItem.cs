using Global;
using UnityEngine;

namespace IncrementalMode.Shop
{
    public class ReLifeShopItem : ActiveShopItem
    {
        [SerializeField] private Entity entity;

        private int _life;
        protected override void OnStart()
        {
            base.OnStart();
            _life = LocalStorage.GetValue("life", 1);
            NewLife();
        }

        protected override void AwakeCall()
        {
            base.AwakeCall();
            Entity.OnEntityReLife += Die;
        }

        protected override void OnDestroyCall()
        {
            base.OnDestroyCall();
            Entity.OnEntityReLife -= Die;
        }

        private void NewLife()
        {
            entity.AdditionalLife = _life;
            LocalStorage.SetValue("life", _life);
            
            if (_life == 0)
            {
                ActiveOff();
                return;
            }
            ActiveOn();
        }
        private void Die()
        {
            _life--;
            NewLife();
        }
        public override void Click()
        {
            if(!CanBuy()) return;
            _life++;
            NewLife();
        }

        public override void OpenInfo()
        {
            item.otherText = _life.ToString();
            base.OpenInfo();
        }
    }
}