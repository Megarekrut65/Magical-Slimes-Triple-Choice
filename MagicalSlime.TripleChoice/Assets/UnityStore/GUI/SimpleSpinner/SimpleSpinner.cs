using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityStore.GUI.SimpleSpinner
{
    [RequireComponent(typeof(Image))]
    public class SimpleSpinner : MonoBehaviour
    {
        [FormerlySerializedAs("Rotation")] [Header("Rotation")]
        public bool rotation = true;

        [FormerlySerializedAs("RotationSpeed")] [Range(-10, 10), Tooltip("Value in Hz (revolutions per second).")]
        public float rotationSpeed = 1;

        [FormerlySerializedAs("RotationAnimationCurve")]
        public AnimationCurve rotationAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [FormerlySerializedAs("Rainbow")] [Header("Rainbow")]
        public bool rainbow = true;

        [FormerlySerializedAs("RainbowSpeed")] [Range(-10, 10), Tooltip("Value in Hz (revolutions per second).")]
        public float rainbowSpeed = 0.5f;

        [FormerlySerializedAs("RainbowSaturation")] [Range(0, 1)]
        public float rainbowSaturation = 1f;

        [FormerlySerializedAs("RainbowAnimationCurve")]
        public AnimationCurve rainbowAnimationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [FormerlySerializedAs("RandomPeriod")] [Header("Options")]
        public bool randomPeriod = true;

        private Image _image;
        private float _period;

        public void Start()
        {
            _image = GetComponent<Image>();
            _period = randomPeriod ? Random.Range(0f, 1f) : 0;
        }

        public void Update()
        {
            if (rotation)
            {
                transform.localEulerAngles = new Vector3(0, 0,
                    -360 * rotationAnimationCurve.Evaluate((rotationSpeed * Time.time + _period) % 1));
            }

            if (rainbow)
            {
                _image.color = Color.HSVToRGB(rainbowAnimationCurve.Evaluate((rainbowSpeed * Time.time + _period) % 1),
                    rainbowSaturation, 1);
            }
        }
    }
}