﻿using Global.Entity;
using Global.Hats;
using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.Lobby.PrivateLobby
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Text usernameText;
        [SerializeField] private SpriteRenderer slimeImage;
        [SerializeField] private SpriteRenderer hatImage;
        
        [SerializeField] private Animator animator;
        private static readonly int ComeTrigger = Animator.StringToHash("Come");

        public void Come(UserInfo info)
        {
            usernameText.text = info.name;
            Hat hat = HatsList.GetHat(info.hat);
            if (hat != null) hatImage.sprite = hat.icon;

            EntityData data = EntityList.GetEntity(info.slimeType);
            if (data != null)
            {
                slimeImage.sprite = data.idleIcon;
                animator.runtimeAnimatorController = data.comeController;
            }
            
            animator.SetTrigger(ComeTrigger);
        }
    }
}