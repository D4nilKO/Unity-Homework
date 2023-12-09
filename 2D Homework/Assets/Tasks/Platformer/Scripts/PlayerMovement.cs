using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string MovingAxisName = "Horizontal";

        [SerializeField] private Transform _groundChecker;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 15f;

        [SerializeField] private LayerMask _groundLayerMask;
        private float _rayLenght = 0.1f;
        private Rigidbody2D _rigidbody;

        private SpriteRenderer _spriteRenderer;
        private PlayerAnimator _playerAnimator;

        public bool IsGrounded =>
            Physics2D.Raycast(_groundChecker.position, Vector2.down, _rayLenght, _groundLayerMask);

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<PlayerAnimator>();
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

            _playerAnimator.SetSpeed(directionX);
            _playerAnimator.SetGrounded();
        }

        private void TryJump()
        {
            if (IsGrounded)
            {
                Jump();

                _playerAnimator.SetGrounded();
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}