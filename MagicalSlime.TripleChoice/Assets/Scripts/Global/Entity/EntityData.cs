﻿using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Global.Entity
{
    /// <summary>
    /// Data for some entity type
    /// </summary>
    [Serializable]
    public class EntityData
    {
        public string key;

        public Sprite idleIcon;
        
        public RuntimeAnimatorController idleController;
        public RuntimeAnimatorController clickController;
        
        public RuntimeAnimatorController comeController;
        
        public RuntimeAnimatorController leftFightController;
        public RuntimeAnimatorController rightFightController;
    }
}