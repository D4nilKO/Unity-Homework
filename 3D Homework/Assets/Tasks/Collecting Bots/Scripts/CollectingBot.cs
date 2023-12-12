using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class CollectingBot : MonoBehaviour
    {
        private Transform _home;
        private Resource _resource;
        
        private MoverToTarget _moverToTarget;
        
        public bool IsFree { get; private set; }

        private void Start()
        {
            _moverToTarget = GetComponent<MoverToTarget>();
            
            SetHome(transform.parent);
            
            IsFree = true;
        }

        private void Update()
        {
            IdlenessСheck();
        }

        private void IdlenessСheck()
        {
            if ((IsFree == false) && (_resource == null))
            {
                SetFree();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            TryCollectResource(other);
        }
        
        public void SetTarget(Resource target)
        {
            _resource = target;
            _moverToTarget.SetTarget(target.transform);
            IsFree = false;
        }

        public void SetFree()
        {
            _moverToTarget.Stop();
            IsFree = true;
        }

        public void SetHome(Transform home)
        {
            _home = home;
            transform.parent = home;
        }

        private void GoHome()
        {
            SetTarget(_home);
        }

        private void SetTarget(Transform target)
        {
            _moverToTarget.SetTarget(target);
            IsFree = false;
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