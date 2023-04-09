using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Global.Entity
{
    [Serializable]
    public class EntityData
    {
        public string key;
        public RuntimeAnimatorController idleController;
        public RuntimeAnimatorController clickController;
    }
}