using Global.Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Global.Button
{
    /// <summary>
    /// Class that add to object button effects
    /// </summary>
    public class ButtonEffect
    {
        /// <summary>
        /// Transform of button
        /// </summary>
        private readonly Transform _transform;

        /// <summary>
        /// Normal scale of button
        /// </summary>
        private readonly Vector3 _scale;

        /// <summary>
        /// Event that be played after button down
        /// </summary>
        private readonly UnityEvent _downEvent;

        /// <summary>
        /// Event that be played after button up
        /// </summary>
        private readonly UnityEvent _upEvent;

        /// <summary>
        /// True if need play sound false else
        /// </summary>
        private readonly bool _needSound;

        /// <summary>
        /// Index of sound in SoundManager
        /// </summary>
        private readonly int _soundIndex;

        /// <param name="transform">of button</param>
        /// <param name="downEvent">play when button down</param>
        /// <param name="upEvent">play when button up</param>
        /// <param name="needSound">true if need play sound else fasle</param>
        /// <param name="soundIndex">index of sound in SoundManager</param>
        public ButtonEffect(Transform transform, UnityEvent downEvent = null, UnityEvent upEvent = null,
            bool needSound = false, int soundIndex = 0)
        {
            this._transform = transform;
            this._downEvent = downEvent;
            this._upEvent = upEvent;
            this._scale = transform.localScale;
            this._needSound = needSound;
            this._soundIndex = soundIndex;
        }

        /// <summary>
        /// Calls down button effects
        /// </summary>
        public void Down()
        {
            _transform.localScale = 1.1f * _scale;
            _downEvent?.Invoke();
            if (_needSound) SoundManager.PlaySound(_soundIndex);
        }

        /// <summary>
        /// Calls up button effects
        /// </summary>
        public void Up()
        {
            _transform.localScale = _scale;
            _upEvent?.Invoke();
        }
    }
}