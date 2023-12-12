using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(BotsBase))]
    public class BaseScanner : MonoBehaviour
    {
        [SerializeField] private Vector3 _homeArea;
        [SerializeField] private Vector3 _collectionArea;

        [SerializeField] private LayerMask _resourceLayer;

        private BotsBase _botsBase;

        private int _maxResourcesInScan = 10;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _homeArea);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _collectionArea);
        }

        private void Start()
        {
            _botsBase = GetComponent<BotsBase>();
        }

        public List<Resource> FindFreeResources()
        {
            Collider[] allResources = new Collider[_maxResourcesInScan];

            int size = Physics.OverlapBoxNonAlloc(transform.position, _collectionArea / 2, allResources,
                Quaternion.identity,
                _resourceLayer);

            List<Resource> freeResources = new();

            for (int i = 0; i < size; i++)
            {
                Collider resourceCollider = allResources[i];
                Resource resource = resourceCollider.GetComponent<Resource>();

                if ((_botsBase.FreeResources.Contains(resource) == false) &&
                    (_botsBase.BusyResources.Contains(resource) == false))
                {
                    freeResources.Add(resource);
                }
            }

            return freeResources;
        }

        public List<Resource> FindResourcesToCollectInHomeArea()
        {
            Collider[] results = new Collider[_maxResourcesInScan];

            int size = Physics.OverlapBoxNonAlloc(transform.position, _homeArea / 2, results, Quaternion.identity,
                _resourceLayer);

            return results.Select(resource => resource.GetComponent<Resource>()).Take(size).ToList();
        }
    }
}