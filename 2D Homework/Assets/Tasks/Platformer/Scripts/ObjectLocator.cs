using System;
using System.Linq;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class ObjectLocator : MonoBehaviour
    {
        [SerializeField] private float _visionRange = 10f;

        [SerializeField] private LayerMask _targetLayerMask;

        private int _maxCountFoundObjects = 10;

        private bool _isTargetAlreadyFounded;

        public event Action<GameObject> TargetFounded;
        public event Action TargetLost;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _visionRange);
        }

        public void TrackingLocate()
        {
            TryLocate(out GameObject target);

            if (target != null && _isTargetAlreadyFounded == false)
            {
                _isTargetAlreadyFounded = true;
                TargetFounded?.Invoke(target.gameObject);

                Debug.Log("Target founded");
            }

            if (target == null && _isTargetAlreadyFounded)
            {
                TargetLost?.Invoke();
                _isTargetAlreadyFounded = false;

                Debug.Log("Target lost");
            }
        }

        public bool TryLocate(out GameObject target)
        {
            Collider2D[] allObjects = new Collider2D[_maxCountFoundObjects];

            int size = Physics2D.OverlapCircleNonAlloc(transform.position, _visionRange, allObjects, _targetLayerMask);

            target = allObjects.Take(size)
                .OrderBy(objectCollider => Vector2.Distance(transform.position, objectCollider.transform.position))
                .FirstOrDefault()?.gameObject;

            return target;
        }
    }
}