using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayer : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash(nameof(Speed));
    private static readonly int Punch = Animator.StringToHash(nameof(Punch));
    
    [SerializeField] [Range(0f, 4f)] private float _speed;
    [SerializeField] private bool _punch;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _speed);

        if (_punch)
        {
            _animator.SetTrigger(Punch);
            _punch = false;
        }
    }
}