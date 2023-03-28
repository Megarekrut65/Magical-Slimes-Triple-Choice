using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace IncrementalMode.AutoFarming
{
    public class StarsController : MonoBehaviour
    {
        [SerializeField] private Color[] colors;
        
        [SerializeField] private Image[] stars;
        [SerializeField] private Image background;
        
        [SerializeField] private Sprite noneStar;
        [SerializeField] private Sprite halfStar;
        [SerializeField] private Sprite fullStar;

        private int MaxStartsLevel => stars.Length * 2;
        public int MaxLevel => colors.Length * MaxStartsLevel;

        
        public void SetStars(int level)
        {
            if (level > MaxLevel) return;
            Color color = colors[level / (MaxStartsLevel+1)];

            Color backgroundColor = new Color(color.r, color.g, color.b, 0.2f);
            background.color = backgroundColor;
            
            int starsLevel = level % (MaxStartsLevel+1);
            int halfLevel = starsLevel / 2;
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].color = color;
                if (i < halfLevel)
                {
                    stars[i].sprite = fullStar;
                }else if (i == halfLevel && starsLevel % 2 != 0)
                {
                    stars[i].sprite = halfStar;
                }
                else
                {
                    stars[i].sprite = noneStar;
                }
            }
        }
        
    }
}