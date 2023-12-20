using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class ResourcesDatabase : MonoBehaviour
    {
        [SerializeField] private List<Resource> _reservedResources = new();

        public void ReserveResource(Resource resource)
        {
            _reservedResources.Add(resource);
        }

        public bool CheckResourceForReserve(Resource resource)
        {
            return _reservedResources.Contains(resource);
        }
    }
}