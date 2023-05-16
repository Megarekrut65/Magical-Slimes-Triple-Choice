﻿using UnityEngine;

namespace UnityStore.GUI.ElementalMeters.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private BoxCollider2D _propBoxCollider;
        public MeterScript healthMeter; //meter code
        public int currentHealth; //meter code
        public int maxHealth = 80; //meter code
        [SerializeField] private float speed;
        private Rigidbody2D _rb2D;

        // Start is called before the first frame update
        void Start()
        {
            _propBoxCollider = GetComponent<BoxCollider2D>();
            _rb2D = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth; //meter code
            healthMeter.SetMaxHealth(maxHealth); //meter code
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            healthMeter.SetHealth(currentHealth); //meter code

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            _rb2D.AddForce(movement * speed);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "DamageSource")
            {
                currentHealth -= 10; //meter code
            }
        }
    }
}