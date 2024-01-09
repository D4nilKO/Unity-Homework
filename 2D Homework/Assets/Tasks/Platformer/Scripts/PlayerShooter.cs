using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot(GetDirection());
            }
        }

        private void Shoot(Vector3 direction)
        {
            Instantiate(_projectilePrefab, transform.position, Quaternion.identity).Init(direction);
        }

        private Vector3 GetDirection()
        {
            return _spriteRenderer.flipX switch
            {
                true => Vector3.left,
                false => Vector3.right
            };
        }
    }
}