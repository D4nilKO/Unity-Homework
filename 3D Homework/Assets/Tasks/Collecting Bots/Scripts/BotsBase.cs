﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(BaseScanner))]
    [RequireComponent(typeof(BotsGarage))]
    public class BotsBase : MonoBehaviour
    {
        [SerializeField] private Transform _resourcesParent;

        [SerializeField] [Min(0.1f)] private float _timeBetweenBaseTicks = 0.5f;

        [SerializeField] private int _botCoast = 3;
        [SerializeField] private ResourceMaterial _resourceToCreateBot;

        private List<Resource> _freeResources = new();
        private List<Resource> _busyResources = new();

        private Dictionary<ResourceMaterial, int> _resourcesValues = new();

        private BaseScanner _scanner;
        private BotsGarage _garage;

        private Coroutine _currentWork;

        private void Start()
        {
            _scanner = GetComponent<BaseScanner>();
            _garage = GetComponent<BotsGarage>();

            SetStartValuesForResources();
            _currentWork = StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            WaitForSeconds waitTime = new(_timeBetweenBaseTicks);

            while (true)
            {
                AddFreeResources();
                SetWorkForAllBots();
                
                yield return waitTime;
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_currentWork);
        }

        public void PickUpResource(Resource resource)
        {
            AddResourceValue(resource);

            resource.transform.parent = _resourcesParent;
            resource.gameObject.SetActive(false);
        }

        private bool CheckAvailabilityInLists(Resource resource)
        {
            return _freeResources.Contains(resource) || _busyResources.Contains(resource);
        }

        private bool TryCreateBot()
        {
            if (TrySpendResourceValue(_resourceToCreateBot, _botCoast))
            {
                _garage.CreateBot();
                return true;
            }

            return false;
        }

        private bool TrySpendResourceValue(ResourceMaterial resourceMaterial, int amount)
        {
            if (_resourcesValues[resourceMaterial] >= amount)
            {
                _resourcesValues[resourceMaterial] -= amount;
                ShowResource(resourceMaterial);

                return true;
            }

            return false;
        }

        private void AddResourceValue(Resource resource)
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

        private void AddFreeResources()
        {
            List<Resource> resources = _scanner.FindResourcesInCollectionArea();

            foreach (Resource resource in resources.Where(resource => CheckAvailabilityInLists(resource) == false))
            {
                _freeResources.Add(resource);
            }
        }

        private void SetWorkForAllBots()
        {
            int freeBotsCount = _garage.GetFreeBotsCount();

            for (int i = 0; i < freeBotsCount; i++)
            {
                SetWorkForBot();
            }
        }

        private void SetWorkForBot()
        {
            if (_garage.TryGetFreeBot(out CollectingBot freeBot) && TryGetFreeResource(out Resource freeResource))
            {
                freeBot.SetTarget(freeResource);
                _busyResources.Add(freeResource);
                _freeResources.Remove(freeResource);
            }
        }

        private bool TryGetFreeResource(out Resource resource)
        {
            resource = _freeResources.FirstOrDefault();

            return resource != null;
        }
    }
}