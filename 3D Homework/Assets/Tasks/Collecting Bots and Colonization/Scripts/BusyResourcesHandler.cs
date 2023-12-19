using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class BusyResourcesHandler : MonoBehaviour
    {
        private List<Resource> _busyResources = new();

        public void AddResource(Resource resource)
        {
            _busyResources.Add(resource);
        }
        
        
    }
}