using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IncrementalMode
{
    public class ShapeController : MonoBehaviour
    {
        [SerializeField] private AudioSource metalAudio;
        [SerializeField] private Animator[] shapes;
        [SerializeField] private Animator animator;
        [SerializeField] private Entity mainCharacter;
        [SerializeField] private int damage;

        private int _hp;
        private static readonly int Active = Animator.StringToHash("IsActive");
        private static readonly int Die = Animator.StringToHash("Die");

        public bool IsActive { get; private set; }
        private void Start()
        {
            _hp = shapes.Length * 5;
            IsActive = false;
            StartCoroutine(Waiting());
            StartCoroutine(Damaging());
        }

        private IEnumerator Waiting()
        {
            while (true)
            {
                int time = Random.Range(10, 20);
                yield return new WaitForSeconds(time);
                if(IsActive) continue;
                foreach (Animator shape in shapes)
                {
                    shape.SetBool(Die, false);
                }
                _hp = shapes.Length * 5;
                IsActive = true;
                animator.SetBool(Active, true);
            }
        }

        private IEnumerator Damaging()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                if(IsActive) mainCharacter.TakeDamage(damage);
            }
        }

        public bool Hit()
        {
            if(!IsActive) return false;
            _hp--;

            if (_hp % 5 != 0) return false;
            shapes[_hp / 5].SetBool(Die, true);
            metalAudio.Play();

            if(_hp != 0) return false;
            IsActive = false;
            animator.SetBool(Active, false);

            return true;
        }
    }
}