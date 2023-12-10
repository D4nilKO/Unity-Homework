using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    public class BotsBase : MonoBehaviour
    {
        [SerializeField] private List<CollectingBot> _collectingBots = new();
        [SerializeField] private List<Resource> _freeResources = new();
        
        [SerializeField] private LayerMask _resourceLayer;
        [SerializeField] private LayerMask _botsLayer;
        
        [SerializeField] private Vector3 _homeArea;
        [SerializeField] private Vector3 _collectionArea;

        private void Start()
        {
            FindMyBots();
            FindFreeResources();
        }

        private void FindFreeResources()
        {
            Collider[] foundedResources = Physics.OverlapBox(transform.position, _collectionArea, Quaternion.identity, _resourceLayer);
            
            foreach (Collider resourceCollider in foundedResources)
            {
                Resource resource = resourceCollider.GetComponent<Resource>();
                
                if (_freeResources.Contains(resource) == false)
                {
                    _freeResources.Add(resource);
                }
            }
        }
        
        private void FindMyBots()
        {
            Collider[] foundedBots = Physics.OverlapBox(transform.position, _homeArea, Quaternion.identity, _botsLayer);
            
            foreach (Collider botCollider in foundedBots)
            {
                CollectingBot collectingBot = botCollider.GetComponent<CollectingBot>();
                
                if (_collectingBots.Contains(collectingBot) == false)
                {
                    _collectingBots.Add(collectingBot);
                    collectingBot.SetHome(transform);
                }
            }
        }

        private bool GetFreeBot(out CollectingBot freeBot)
        {
            freeBot = _collectingBots.FirstOrDefault(bot => bot.Free);

            return freeBot != null;
        }

        private bool GetFreeResource(out Resource freeResource)
        {
            freeResource = _freeResources.FirstOrDefault();
            
            return freeResource != null;
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