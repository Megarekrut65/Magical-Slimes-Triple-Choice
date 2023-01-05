using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicking : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private Animator _animator;

    [SerializeField] 
    private float _addingSpeed;

    private bool _isClicking = false;

    private void Start()
    {
        StartCoroutine(DecreaseSpeed());
    }

    private IEnumerator DecreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!_isClicking)
            {
                _animator.speed = 0;
                _animator.StopPlayback();
            }
            _isClicking = false;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.StartPlayback();
        _animator.speed += _addingSpeed;
        _isClicking = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
