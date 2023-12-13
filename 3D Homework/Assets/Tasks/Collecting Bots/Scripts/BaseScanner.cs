using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class BaseScanner : MonoBehaviour
    {
        [SerializeField] private Vector3 _homeArea;
        [SerializeField] private Vector3 _collectionArea;

        [SerializeField] private LayerMask _resourceLayer;

        private int _maxResourcesInScan = 10;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _homeArea);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _collectionArea);
        }

        public List<Resource> FindResourcesInCollectionArea()
        {
            return FindResources(_collectionArea);
        }

        private List<Resource> FindResources(Vector3 scanArea)
        {
            Collider[] results = new Collider[_maxResourcesInScan];

            int size = Physics.OverlapBoxNonAlloc(transform.position, scanArea / 2, results, Quaternion.identity,
                _resourceLayer);

            return results.Select(resource => resource.GetComponent<Resource>()).Take(size).ToList();
        }
    }
}