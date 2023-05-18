using System.Collections;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace IncrementalMode
{
    /// <summary>
    /// Controls speed changing and displaying it in GUI.
    /// </summary>
    public class SpeedController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Slider speedSlider;
        [SerializeField] private Text speedText;
        [SerializeField] private Image sliderFill;

        [SerializeField] private float speedValue;

        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;

        private float _increasePercent = 1f;
        public float IncreasePercent
        {
            get => _increasePercent;
            set
            {
                if (value > 0f)
                {
                    _increasePercent = value;
                }
            }
        }
        public float Percent => IncreasePercent * (speedSlider.value-1) / (speedSlider.maxValue-1);
        
        
        private void Start()
        {
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            
            animator.speed = DataSaver.LoadSpeed() / 2;
            Increase();
            
            StartCoroutine(DecreaseSpeed());
        }

        public void Increase()
        {
            if (animator.speed >= maxSpeed) return;
            float speed = animator.speed;
            speed += speedValue / 7;
            animator.speed = speed;
            SliderValue(speed);
        }

        private IEnumerator DecreaseSpeed()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (animator.speed <= minSpeed) continue;
                float speed = animator.speed;
                speed -= speedValue / 20;
                animator.speed = speed;
                SliderValue(speed);
            }
        }

        private Color Rgb(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }
        private void SliderValue(float value)
        {
            DataSaver.SaveSpeed(value);
            
            speedSlider.value = value;
            sliderFill.color = Percent switch
            {
                >= 1.95f => Rgb(178,34,34),
                >= 1.75f => Rgb(220,20,60),
                >= 1.35f => Rgb(255,0,0),
                >= 0.96f => Rgb(245, 80, 80),
                >= 0.7f => Rgb(252, 115, 0),
                >= 0.45f => Rgb(252, 226, 42),
                > 0.01f => Rgb(191, 219, 56),
                _ => new Color(0,0,0,255)
            };
            speedText.color = sliderFill.color;
            speedText.text = $"X{2 * Percent:0.0}".Replace(',', '.');
        }
    }
}