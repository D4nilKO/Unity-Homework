using UnityEngine;

namespace Tasks.Enemy_Spawner
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] [Range(1, 100)] private float _speed;
        private Vector3 _direction;

        public void Init(Vector3 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _direction * _speed);
        }
    }
}