using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string MovingAxisName = "Horizontal";

        public readonly int Speed = Animator.StringToHash(nameof(Speed));
        public readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));

        [SerializeField] private Transform _groundChecker;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 15f;

        [SerializeField] private LayerMask _groundLayerMask;
        private float _rayLenght = 0.1f;
        private Rigidbody2D _rigidbody;

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private bool _isJumping;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryJump();
            }
        }

        private void Move()
        {
            float directionX = Input.GetAxis(MovingAxisName);
            _rigidbody.velocity = new Vector2(directionX * _speed, _rigidbody.velocity.y);

            switch (directionX)
            {
                case < 0:
                    _spriteRenderer.flipX = true;
                    break;
                case > 0:
                    _spriteRenderer.flipX = false;
                    break;
            }

            _animator.SetFloat(Speed, Mathf.Abs(directionX));
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if ((_groundLayerMask.value & (1 << col.gameObject.layer)) != 0)
            {
                _isJumping = false;
                _animator.SetBool(IsJumping, _isJumping);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if ((_groundLayerMask.value & (1 << other.gameObject.layer)) != 0)
            {
                _isJumping = true;
                _animator.SetBool(IsJumping, _isJumping);
            }
        }

        private void TryJump()
        {
            RaycastHit2D hit = Physics2D.Raycast(_groundChecker.position, Vector2.down, _rayLenght, _groundLayerMask);

            if (hit)
            {
                Jump();

                _isJumping = true;
                _animator.SetBool(IsJumping, _isJumping);
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}