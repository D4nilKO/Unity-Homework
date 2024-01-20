using System;
using System.Linq;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class ObjectLocator : MonoBehaviour
    {
        [SerializeField] private float _visionRange = 10f;

        [SerializeField] private LayerMask _targetLayerMask;

        public bool IsTargetAlreadyFounded { get; private set; }

        public event Action<GameObject> TargetFounded;
        public event Action TargetLost;

        public void Locate()
        {
            Debug.Log("Locate");

            Collider2D[] allObjects =
                Physics2D.OverlapCircleAll(transform.position, _visionRange, _targetLayerMask);

            Collider2D target = allObjects.OrderBy(obj => Vector2.Distance(transform.position, obj.transform.position))
                .FirstOrDefault();

            if (target != null && IsTargetAlreadyFounded == false)
            {
                
            }
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