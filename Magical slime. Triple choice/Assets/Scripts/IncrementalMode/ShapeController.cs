using System;
using System.Collections;
using Global;
using Global.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

namespace IncrementalMode
{
    public class ShapeController : MonoBehaviour
    {
        public delegate void Spawning();

        public static event Spawning OnSpawning;
        
        [SerializeField] private Animator[] shapes;
        [SerializeField] private Animator animator;
        [SerializeField] private Entity mainCharacter;
        [SerializeField] private int damage;

        private const int MinDelay = 10;//30
        private const int MaxDelay = 15;//60
        public static int ShapeTime => Random.Range(MinDelay, MaxDelay);
        private int _hp;
        private static readonly int Active = Animator.StringToHash("IsActive");
        private static readonly int Die = Animator.StringToHash("Die");
        
        public bool IsShield { get; set; }

        public bool IsActive { get; private set; }
        private void Start()
        {
            _hp = shapes.Length * 5;
            IsActive = false;
            StartCoroutine(Waiting());
            StartCoroutine(Damaging());
        }

        private void Awake()
        {
            Entity.OnEntityReLife += ReLife;
        }

        private void OnDestroy()
        {
            Entity.OnEntityReLife -= ReLife;
        }

        private void ReLife()
        {
            StartCoroutine(DestroyShapes());
        }

        private IEnumerator DestroyShapes()
        {
            while (!Hit())
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        private IEnumerator Waiting()
        {
            int time = DataSaver.LoadShapeTime() + 1;
            while (true)
            {
                while (IsActive)
                {
                    yield return new WaitForSeconds(1f);
                }
                
                if (time <= 0) time = ShapeTime;
                
                for (int i = 1; i <= time; i++)
                {
                    yield return new WaitForSeconds(1f);
                    DataSaver.SaveShapeTime(time - i);
                }
                time = 0;
                
                if(mainCharacter.IsDied) break;
                if(IsActive) continue;

                foreach (Animator shape in shapes)
                {
                    shape.SetBool(Die, false);
                }
                _hp = shapes.Length * 5;
                IsActive = true;
                animator.SetBool(Active, true);
                
                OnSpawning?.Invoke();
            }
        }

        private IEnumerator Damaging()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                if(mainCharacter.IsDied) break;
                if (IsActive && IsShield)
                {
                    SoundManager.PlaySound(4);
                } 
                else if (IsActive && !IsShield)
                {
                    SoundManager.PlaySound(3);
                    mainCharacter.TakeDamage(damage * _hp/5 + 1);
                }
            }
        }

        /// <summary>
        /// Gives damage to shapes
        /// </summary>
        /// <returns>True - if shapes were destroyed. False else</returns>
        public bool Hit()
        {
            if (mainCharacter.IsDied) return false;
            if(!IsActive) return false;
            _hp--;

            if (_hp % 5 != 0) return false;
            shapes[_hp / 5].SetBool(Die, true);

            if(_hp != 0) return false;
            IsActive = false;
            animator.SetBool(Active, false);
            return true;
        }
        
    }
}