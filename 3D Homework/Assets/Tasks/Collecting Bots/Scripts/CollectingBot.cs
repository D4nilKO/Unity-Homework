using UnityEngine;

namespace Tasks.Collecting_Bots.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class CollectingBot : MonoBehaviour
    {
        [SerializeField] private Transform _home;
        [SerializeField] private Resource _resource;

        private MoverToTarget _moverToTarget;

        public bool IsFree { get; private set; }

        private void Start()
        {
            _moverToTarget = GetComponent<MoverToTarget>();
            _home = transform.parent;
            IsFree = true;
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