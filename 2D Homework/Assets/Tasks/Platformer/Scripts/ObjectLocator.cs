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

        public bool IsTargetAlreadyFounded { get; private set; }

        public event Action<GameObject> TargetFounded;
        public event Action TargetLost;

        public void TrackingLocate()
        {
            GameObject target = Locate();

            if (target != null && IsTargetAlreadyFounded == false)
            {
                IsTargetAlreadyFounded = true;
                TargetFounded?.Invoke(target.gameObject);

                Debug.Log("Target founded");
            }

            if (target == null && IsTargetAlreadyFounded)
            {
                TargetLost?.Invoke();
                IsTargetAlreadyFounded = false;

                Debug.Log("Target lost");
            }
        }

        public GameObject Locate()
        {
            Collider2D [] allObjects = new Collider2D[_maxCountFoundObjects];
            
            int size = Physics2D.OverlapCircleNonAlloc(transform.position, _visionRange, allObjects, _targetLayerMask);

            GameObject target = allObjects.Take(size)
                .OrderBy(objectCollider => Vector2.Distance(transform.position, objectCollider.transform.position))
                .FirstOrDefault()?.gameObject;

            Debug.Log("Target: " + (target != null ? target.name : null));
            return target;
        }

        public void ClearFlag()
        {
            IsTargetAlreadyFounded = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _visionRange);
        }
    }
}