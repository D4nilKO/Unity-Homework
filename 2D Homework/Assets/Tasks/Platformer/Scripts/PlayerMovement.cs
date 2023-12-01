using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string MovingAxisName = "Horizontal";
        
[SerializeField] private Transform _groundChecker;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 15f;

        [SerializeField] private LayerMask _layerMask;

        private Rigidbody2D _rigidbody;

        private float _rayLenght = 0.1f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GroundChecking();
            }
        }

        private void Move()
        {
            float directionX = Input.GetAxis(MovingAxisName);
            _rigidbody.velocity = new Vector2(directionX * _speed, _rigidbody.velocity.y);
        }

        private void GroundChecking()
        {
            RaycastHit2D hit = Physics2D.Raycast(_groundChecker.position, Vector2.down, _rayLenght, _layerMask);
            
            if (hit)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}