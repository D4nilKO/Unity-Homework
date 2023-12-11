using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class CollectingBot : MonoBehaviour
    {
        [SerializeField] private Transform _home;
        [SerializeField] private Resource _resource;
        
        public bool _isReached;
        
        private bool _goingHome;
        private bool _hasResource;
        
        private MoverToTarget _moverToTarget;
        
        public bool Free { get; private set; }

        private void Start()
        {
            _moverToTarget = GetComponent<MoverToTarget>();
            Free = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            TryCollectResource(other);
        }
        
        private void TryCollectResource(Component other)
        {
            if (other.TryGetComponent(out Resource resource) && _hasResource == false)
            {
                if (resource == _resource)
                {
                    _resource.transform.parent = gameObject.transform;
                    _hasResource = true;
                
                    GoHome();
                }
            }
        }

        public void SetHome(Transform home)
        {
            _home = home;
            transform.parent = home;
        }

        private void GoHome()
        {
            SetTarget(_home);
            _goingHome = true;
        }

        public void SetTarget(Resource target)
        {
            _resource = target;
            _moverToTarget.SetTarget(target.transform);
            Free = false;
        }
        
        public void SetTarget(Transform target)
        {
            _moverToTarget.SetTarget(target);
            Free = false;
        }

        public void SetFree()
        {
            _moverToTarget.Stop();
            _hasResource = false;
            Free = true;
        }
    }
}