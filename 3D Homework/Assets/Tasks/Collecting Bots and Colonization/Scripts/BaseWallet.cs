using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class BaseWallet : MonoBehaviour
    {
        private readonly IDictionary<ResourceMaterial, int> _resourcesValues = new Dictionary <ResourceMaterial, int>();

        public int this[ResourceMaterial key]
        {
            get => _resourcesValues[key];

            set
            {
                _resourcesValues[key] = value;
                
                ShowResource(key);
            }
        }

        private int _resourceValueNeededToAction;
        private event Action OnResourceValueCollectedToAction;

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

        private void ShowAllResources()
        {
            foreach (ResourceMaterial resourceMaterial in Enum.GetValues(typeof(ResourceMaterial)))
            {
                ShowResource(resourceMaterial);
            }
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