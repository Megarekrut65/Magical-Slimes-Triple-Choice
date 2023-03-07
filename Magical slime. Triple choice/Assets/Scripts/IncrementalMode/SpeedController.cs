using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace IncrementalMode
{
    public class SpeedController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Slider speedSlider;
        [SerializeField] private Image sliderFill;

        [SerializeField] private float speedValue;

        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
    
        public float Percent => speedSlider.value / speedSlider.maxValue;

        private void Start()
        {
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            StartCoroutine(DecreaseSpeed());
        }

        public void Increase()
        {
            if (!(animator.speed < maxSpeed)) return;
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
                if (!(animator.speed > minSpeed)) continue;
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
            speedSlider.value = value;
            sliderFill.color = Percent switch
            {
                >= 0.96f => Rgb(245, 80, 80),
                >= 0.7f => Rgb(252, 115, 0),
                >= 0.45f => Rgb(252, 226, 42),
                > 0.05f => Rgb(191, 219, 56),
                _ => Color.black
            };
        }
    }
}