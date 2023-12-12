using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(BaseScanner))]
    public class BotsBase : MonoBehaviour
    {
        [SerializeField] private Transform _resourcesParent;

        [SerializeField] private List<CollectingBot> _collectingBots = new();

        [SerializeField] [Min(0.1f)] private float _timeBetweenBaseTicks = 0.5f;

        private List<Resource> _freeResources = new();
        private List<Resource> _busyResources = new();

        private Dictionary<ResourceMaterial, int> _resourcesValues = new();

        private BaseScanner _scanner;

        public IReadOnlyList<Resource> FreeResources => _freeResources.ToList();
        public IReadOnlyList<Resource> BusyResources => _busyResources.ToList();

        private void Start()
        {
            _scanner = GetComponent<BaseScanner>();
            
            SetStartValuesForResources();
            CollectResourcesInHome();

            StartCoroutine(Work());
        }

        public void AddResourceValue(Resource resource)
        {
            _resourcesValues[resource.ResourceType] += resource.Amount;
        }

        private void SetStartValuesForResources()
        {
            foreach (ResourceMaterial resourceMaterial in Enum.GetValues(typeof(ResourceMaterial)))
            {
                _resourcesValues.Add(resourceMaterial, 0);
            }
        }

        private IEnumerator Work()
        {
            WaitForSeconds waitTime = new(_timeBetweenBaseTicks);

            while (true)
            {
                yield return waitTime;

                CollectResourcesInHome();
                AddFreeResources();
                SetWorkForAllBots();
            }
        }

        private void AddFreeResources()
        {
            List<Resource> freeResources = _scanner.FindFreeResources();

            foreach (Resource freeResource in freeResources)
            {
                _freeResources.Add(freeResource);
            }
        }

        private void CollectResourcesInHome()
        {
            List<Resource> resourcesToCollect = _scanner.FindResourcesToCollectInHomeArea();

            foreach (Resource resource in resourcesToCollect)
            {
                if (resource.transform.parent != null)
                {
                    GameObject parent = resource.transform.parent.gameObject;

                    if (parent.TryGetComponent(out CollectingBot bot))
                    {
                        bot.SetFree();
                    }
                }

                AddResourceValue(resource);

                resource.transform.parent = _resourcesParent;
                resource.gameObject.SetActive(false);
            }
        }

        private void SetWorkForAllBots()
        {
            int freeBotsCount = GetFreeBotsCount();

            for (int i = 0; i < freeBotsCount; i++)
            {
                SetWorkForBot();
            }
        }

        private int GetFreeBotsCount()
        {
            return _collectingBots.Count(bot => bot.IsFree);
        }

        private void SetWorkForBot()
        {
            if (TryGetFreeBot(out CollectingBot freeBot) && TryGetFreeResource(out Resource freeResource))
            {
                freeBot.SetTarget(freeResource);
                _busyResources.Add(freeResource);
                _freeResources.Remove(freeResource);
            }
        }

        private bool TryGetFreeBot(out CollectingBot freeBot)
        {
            freeBot = _collectingBots.FirstOrDefault(bot => bot.IsFree);

            return freeBot != null;
        }

        private bool TryGetFreeResource(out Resource resource)
        {
            resource = _freeResources.FirstOrDefault();

            return resource != null;
        }
    }
}