using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class MoverToTarget : MonoBehaviour
    {
        [SerializeField] [Range(1, 10)] private int _speed;
        [SerializeField] private Transform _target;

        public bool IsReached => transform.position == _target.position;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}