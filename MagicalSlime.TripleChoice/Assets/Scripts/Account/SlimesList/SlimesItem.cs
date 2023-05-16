using IncrementalMode;
using UnityEngine;
using UnityEngine.UI;

namespace Account.SlimesList
{
    public class SlimesItem : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Text nameText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text energyText;
        [SerializeField] private Image background;

        public void SetData(SlimeData slimeData, RuntimeAnimatorController controller, Color backgroundColor)
        {
            animator.runtimeAnimatorController = controller;
            nameText.text = slimeData.name;
            levelText.text = slimeData.level.ToString();
            energyText.text = slimeData.energy;
            background.color = backgroundColor;
        }
    }
}