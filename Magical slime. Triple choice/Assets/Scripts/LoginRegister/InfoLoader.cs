using UnityEngine;
using UnityEngine.UI;

namespace LoginRegister
{
    public class InfoLoader : MonoBehaviour
    {
        [SerializeField] private Text slimeNameText;
        [SerializeField] private Text maxLevelText;
        [SerializeField] private Text maxEnergyText;
        [SerializeField] private Text cupsText;
        [SerializeField] private Text diamondsText;
        [SerializeField] private Text energyText;
        [SerializeField] private Text levelText;
        
        public void Load(Info info)
        {
            slimeNameText.text = info.slimeName;
            maxLevelText.text = info.maxLevel.ToString();
            maxEnergyText.text = info.maxEnergy;
            cupsText.text = info.cups.ToString();
            diamondsText.text = info.diamonds.ToString();
            energyText.text = info.energy;
            levelText.text = info.level.ToString();
        }
        
    }
}