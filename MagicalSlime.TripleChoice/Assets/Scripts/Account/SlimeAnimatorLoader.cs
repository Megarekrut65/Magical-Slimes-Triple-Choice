using System;
using Global;
using Global.Entity;
using UnityEngine;

namespace Account
{
    /// <summary>
    /// Loads animator of current slime
    /// </summary>
    public class SlimeAnimatorLoader : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Start()
        {
            animator.runtimeAnimatorController = EntityList.GetEntity(DataSaver.LoadSlimeType()).idleController;
        }
    }
}