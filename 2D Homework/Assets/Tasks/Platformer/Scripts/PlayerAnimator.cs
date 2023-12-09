using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimator: MonoBehaviour
    {
        public readonly int Speed = Animator.StringToHash(nameof(Speed));
        public readonly int Grounded = Animator.StringToHash(nameof(Grounded));
        
        private Animator _animator;
        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            SetGrounded();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            SetGrounded();
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, Mathf.Abs(speed));
        }

        public void SetGrounded()
        {
            _animator.SetBool(Grounded, _playerMovement.IsGrounded);
        }
    }
}