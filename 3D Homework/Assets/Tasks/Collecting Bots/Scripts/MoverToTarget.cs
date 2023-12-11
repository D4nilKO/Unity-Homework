using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class MoverToTarget : MonoBehaviour
    {
        [SerializeField] [Range(1, 10)] private int _speed;
        [SerializeField] private bool _canMoving;

        [SerializeField] private Transform _target;

        public void SetTarget(Transform target)
        {
            _canMoving = true;

            _target = target;
            transform.LookAt(target);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
        }

        private void Update()
        {
            if (_canMoving)
            {
                Move();
            }
        }

        private void Move()
        {
            if (_target == null)
            {
                
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            if (_target != null)
            {
                Gizmos.DrawLine(transform.position, _target.position);
            }
        }

        public void Stop()
        {
            _canMoving = false;
        }
    }
}