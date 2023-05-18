using Global.Entity;
using Global.Hats;
using UnityEngine;

namespace FightingMode.Game
{
    /// <summary>
    /// Loads entity data to GUI
    /// </summary>
    public class EntityLoader : MonoBehaviour
    {
        [SerializeField] private string type;
        [SerializeField] private string position;
        
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private SpriteRenderer hat;

        private void Start()
        {
            UserInfo info = FightingSaver.LoadUserInfo(type);

            EntityData data = EntityList.GetEntity(info.slimeType);
            if (data != null)
            {
                sprite.sprite = data.idleIcon;
                animator.runtimeAnimatorController = position == "left"
                    ? data.leftFightController
                    : data.rightFightController;
            }

            Hat hatData = HatsList.GetHat(info.hat);
            if (hatData != null)
            {
                hat.sprite = hatData.icon;
            }
        }
        
    }
}