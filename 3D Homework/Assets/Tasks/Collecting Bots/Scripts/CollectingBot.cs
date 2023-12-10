using System;
using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class CollectingBot : MonoBehaviour
    {
        [SerializeField] private Transform _home;
        
        [SerializeField] private Resource _resource;
        
        private bool _goingHome;
        private bool _hasResource;
        
        private MoverToTarget _moverToTarget;
        
        public bool Free { get; private set; }

        private void Start()
        {
            _moverToTarget = GetComponent<MoverToTarget>();
        }

        private void OnTriggerEnter(Collider other)
        {
            TryCollectResource(other);
            
        }
        
        private void TryCollectResource(Component other)
        {
            if (other.TryGetComponent(out Resource resource) && _hasResource == false)
            {
                _resource = resource;
                _resource.transform.parent = gameObject.transform;
                _resource.SetCollected();
                _hasResource = true;
            }
        }

        public void SetHome(Transform home)
        {
            _home = home;
            transform.parent = home;
        }
        
        private void DropResource()
        {
            _resource.transform.position = Vector3.zero;
            _resource.transform.parent = _home;
            _hasResource = false;
        }

        public void GoHome()
        {
            SetTarget(_home);
            _goingHome = true;
        }

        private void SetTarget(Transform target)
        {
            _moverToTarget.SetTarget(target);
            Free = false;
        }
    }
}