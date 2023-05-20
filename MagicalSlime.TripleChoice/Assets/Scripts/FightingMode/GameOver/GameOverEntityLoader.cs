using Global.Entity;
using Global.Hats;
using UnityEngine;
using UnityEngine.UI;

namespace FightingMode.GameOver
{
    public class GameOverEntityLoader : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer hatSpriteRenderer;

        [SerializeField] private Text usernameText;
        [SerializeField] private Text slimeNameText;

        [SerializeField] private string type;

        private void Start()
        {
            UserInfo info = FightingSaver.LoadUserInfo(type);
            
            EntityData data = EntityList.GetEntity(info.slimeType);
            if (data != null)
            {
                spriteRenderer.sprite = data.idleIcon;
                animator.runtimeAnimatorController = data.idleController;
            }
            Hat hat = HatsList.GetHat(info.hat);
            if(hat != null) hatSpriteRenderer.sprite = hat.icon;

            usernameText.text = info.name;
            slimeNameText.text = info.slimeName;
        }
    }
}