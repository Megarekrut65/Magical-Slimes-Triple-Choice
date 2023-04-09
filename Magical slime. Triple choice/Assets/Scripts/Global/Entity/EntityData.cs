using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Global.Entity
{
    [Serializable]
    public class EntityData
    {
        public string key;
        public AnimatorController idleController;
        public AnimatorController clickController;
    }
}