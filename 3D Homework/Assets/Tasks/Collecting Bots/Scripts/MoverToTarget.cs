using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class MoverToTarget : MonoBehaviour
    {
        [SerializeField] [Range(1, 10)] private int _speed;
        [SerializeField] private bool _canMoving;
        
        private Transform _target;
        
        public bool IsReached => transform.position == _target.position;

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
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            
            if (IsReached)
            {
                _canMoving = false;
            }
        }
    }
}