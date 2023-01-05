using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedUp : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private float _speed;
    
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.speed = _speed;
    }
}
