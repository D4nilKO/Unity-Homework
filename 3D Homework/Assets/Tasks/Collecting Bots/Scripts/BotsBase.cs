using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class BotsBase : MonoBehaviour
    {
        [SerializeField] private List<CollectingBot> _collectingBots = new();
        [SerializeField] private List<Resource> _allResources = new();

        [SerializeField] private LayerMask _resourceLayer;
        [SerializeField] private LayerMask _botsLayer;

        [SerializeField] private Vector3 _homeArea;
        [SerializeField] private Vector3 _collectionArea;

        [SerializeField] [Min(0.1f)] private float _timeToScanResources = 0.1f;

        [SerializeField] private int _ironCount;
        [SerializeField] private int _woodCount;
        [SerializeField] private int _stoneCount;

        private int _maxResourcesInScan = 10;

        private void Start()
        {
            FindMyBots();
            CollectResourcesOnBase();
            StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            WaitForSeconds waitTime = new(_timeToScanResources);

            while (true)
            {
                yield return waitTime;

                CollectResourcesOnBase();
                FindFreeResources();
                SetWorkForBot();
            }
        }

        private void FindFreeResources()
        {
            Collider[] results = new Collider[_maxResourcesInScan];
            int size = Physics.OverlapBoxNonAlloc(transform.position, _collectionArea/2, results, Quaternion.identity,
                _resourceLayer);

            for (int i = 0; i < size; i++)
            {
                Collider resourceCollider = results[i];
                Resource resource = resourceCollider.GetComponent<Resource>();

                if ((resource.HasBot == false) && (_allResources.Contains(resource) == false))
                {
                    _allResources.Add(resource);
                }
            }
        }

        private void CollectResourcesOnBase()
        {
            Collider[] results = Physics.OverlapBox(transform.position, _homeArea/2, Quaternion.identity, _resourceLayer);

            foreach (Collider resourceCollider in results)
            {
                Resource resource = resourceCollider.GetComponent<Resource>();

                if (resource.transform.parent != null)
                {
                    GameObject parent = resource.transform.parent.gameObject;

                    if (parent.TryGetComponent(out CollectingBot bot))
                    {
                        bot.SetFree();
                    }
                }

                // Как сделать добавление разных ресурсов без switch?
                switch (resource.GetResourceType())
                {
                    case ResourceMaterial.Iron:
                        _ironCount++;
                        break;
                    
                    case ResourceMaterial.Wood:
                        _woodCount++;
                        break;
                    
                    case ResourceMaterial.Stone:
                        _stoneCount++;
                        break;
                    
                    default:
                        Debug.Log("Неожиданный ресурс...");
                        break;
                }

                Destroy(resource.gameObject);
            }
        }

        private void SetWorkForBot()
        {
            if (TryGetFreeBot(out CollectingBot freeBot) && TryGetFreeResource(out Resource freeResource))
            {
                freeBot.SetTarget(freeResource);
                freeResource.SetBusy();
            }
        }

        private void FindMyBots()
        {
            Collider[] results = Physics.OverlapBox(transform.position, _collectionArea/2, Quaternion.identity, _botsLayer);

            foreach (Collider botCollider in results)
            {
                CollectingBot collectingBot = botCollider.GetComponent<CollectingBot>();

                _collectingBots.Add(collectingBot);
                collectingBot.SetHome(transform);
            }
        }

        private bool TryGetFreeBot(out CollectingBot freeBot)
        {
            freeBot = _collectingBots.FirstOrDefault(bot => bot.Free);

            return freeBot != null;
        }

        private bool TryGetFreeResource(out Resource resource)
        {
            resource = _allResources.FirstOrDefault(resource => resource.HasBot == false);

            return resource != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _homeArea);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _collectionArea);
        }
    }
}