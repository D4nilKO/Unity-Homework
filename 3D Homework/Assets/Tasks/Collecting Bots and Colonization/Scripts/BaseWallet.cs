using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class BaseWallet: MonoBehaviour
    {
        private Dictionary<ResourceMaterial, int> _resourcesValues = new();

        private void Awake()
        {
            SetStartValuesForResources();
        }

        public bool TrySpendResourceValue(ResourceMaterial resourceMaterial, int amount)
        {
            if (_resourcesValues[resourceMaterial] >= amount)
            {
                _resourcesValues[resourceMaterial] -= amount;
                ShowResource(resourceMaterial);

                return true;
            }

            return false;
        }

        public void AddResourceValue(Resource resource)
        {
            _resourcesValues[resource.ResourceType] += resource.Amount;
            ShowResource(resource);
        }

        private void ShowResource(Resource resource)
        {
            Debug.Log($"У вас имеется: {resource.ResourceType} -- {_resourcesValues[resource.ResourceType]}");
        }

        private void ShowResource(ResourceMaterial resourceMaterial)
        {
            Debug.Log($"У вас имеется: {resourceMaterial} -- {_resourcesValues[resourceMaterial]}");
        }

        private void SetStartValuesForResources()
        {
            foreach (ResourceMaterial resourceMaterial in Enum.GetValues(typeof(ResourceMaterial)))
            {
                _resourcesValues.Add(resourceMaterial, 0);
            }
        }
    }
}