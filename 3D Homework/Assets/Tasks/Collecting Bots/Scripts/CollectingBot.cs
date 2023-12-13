﻿using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class CollectingBot : MonoBehaviour
    {
        [SerializeField] private BotsBase _base;
        [SerializeField] private Transform _home;
        [SerializeField] private Resource _resource;

        private MoverToTarget _moverToTarget;

        public bool IsFree { get; private set; }

        private bool IsReadyToGiveResource =>
            _moverToTarget.DistanceToTarget == 0 && IsFree == false;

        private void Start()
        {
            _moverToTarget = GetComponent<MoverToTarget>();
            _home = transform.parent;
            _base = _home.GetComponent<BotsBase>();
            IsFree = true;
        }

        private void Update()
        {
            TryToGiveResource();
        }

        public void SetTarget(Resource target)
        {
            _resource = target;
            _moverToTarget.SetTarget(target.transform);
            IsFree = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            TryCollectResource(other);
        }

        private void TryToGiveResource()
        {
            if (IsReadyToGiveResource)
            {
                _base.PickUpResource(_resource);
                SetFree();
            }
        }

        private void SetFree()
        {
            _moverToTarget.Stop();
            _resource = null;
            IsFree = true;
        }

        private void GoHome()
        {
            _moverToTarget.SetTarget(_home);
        }

        private void TryCollectResource(Component other)
        {
            if (other.TryGetComponent(out Resource resource))
            {
                if (resource == _resource)
                {
                    _resource.transform.parent = gameObject.transform;
                    GoHome();
                }
            }
        }
    }
}