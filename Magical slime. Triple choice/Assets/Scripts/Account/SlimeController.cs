using System;
using Global;
using Global.Entity;
using UnityEngine;

namespace Account
{
    public class SlimeController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Start()
        {
            animator.runtimeAnimatorController = EntityList.GetEntity(DataSaver.LoadSlimeType()).idleController;
        }
    }
}