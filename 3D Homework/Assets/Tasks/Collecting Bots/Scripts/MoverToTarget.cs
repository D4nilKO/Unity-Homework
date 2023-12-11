using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class MoverToTarget : MonoBehaviour
    {
        [SerializeField] [Range(1, 10)] private int _speed = 1;
        private bool _canMoving;

        private Transform _target;

        private void Update()
        {
            if (_canMoving)
            {
                Move();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            if (_target != null)
            {
                Gizmos.DrawLine(transform.position, _target.position);
            }
        }

        public void SetTarget(Transform target)
        {
            _canMoving = true;
            _target = target;
            
            transform.LookAt(target);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
        }

        public void Stop()
        {
            _canMoving = false;
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}