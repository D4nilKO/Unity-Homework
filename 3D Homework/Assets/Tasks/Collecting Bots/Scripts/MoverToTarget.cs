using System.Collections;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class MoverToTarget : MonoBehaviour
    {
        [SerializeField] [Range(1, 10)] private int _speed = 5;

        private Transform _target;
        
        private Coroutine _currentMoving;

        private void OnDestroy()
        {
            StopCoroutine(_currentMoving);
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
            _target = target;

            transform.LookAt(target);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
            
            Stop();
            _currentMoving = StartCoroutine(Moving());
        }

        public void Stop()
        {
            if (_currentMoving != null)
            {
                StopCoroutine(_currentMoving);
            }
        }

        private IEnumerator Moving()
        {
            while (transform.position != _target.position)
            {
                Move();
                yield return null;
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }
}