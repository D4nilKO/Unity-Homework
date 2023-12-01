using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string MovingAxisName = "Horizontal";

        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 4f;

        [SerializeField] private ContactFilter2D _contactFilter;
        
        private Rigidbody2D _rigidbody;
        private RaycastHit2D[] _hits = new RaycastHit2D[1];
        
        [SerializeField]private bool _canJumping = true;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            Vector3 direction = GetDirection();
            float distance = _speed * Time.deltaTime;

            if (_rigidbody.Cast(direction, _contactFilter, _hits, distance) == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, distance);
            }
        }

        private void Jump()
        {
            if (_canJumping == false)
            {
                Debug.Log("can not jump");
                return;
            }
            
            if (Input.GetKey(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _canJumping = false;
            }
        }
        
        public void OnGround()
        {
            _canJumping = true;
        }

        private Vector3 GetDirection()
        {
            float inputDirectionValue = Input.GetAxis(MovingAxisName);

            Vector3 direction = inputDirectionValue switch
            {
                > 0 => Vector3.right,
                < 0 => Vector3.left,
                _ => Vector3.zero
            };

            return direction;
        }
    }
}