using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    [RequireComponent(typeof(MoverToTarget))]
    public class CollectingBot : MonoBehaviour
    {
        [SerializeField] private BotsBase _base;
        [SerializeField] private Transform _home;
        [SerializeField] private Resource _resource;
        [SerializeField] private GameObject _basePrefab;

        private MoverToTarget _mover;
        private BaseBuilder _builder;

        public GameObject GetBasePrefab => _basePrefab;

        public bool IsFree { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<MoverToTarget>();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            _mover.OnTargetReached += GiveResource;
        }

        private void OnDisable()
        {
            _mover.OnTargetReached -= GiveResource;
            
            if (_builder!= null)
            {
                _builder.OnBaseBuilt -= ChangeModeToCollectingResource;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            TryCollectResource(other);
        }

        public void Initialize()
        {
            _mover = GetComponent<MoverToTarget>();
            _home = transform.parent;

            _base = _home.GetComponent<BotsBase>();

            IsFree = true;
        }

        public void SetTarget(Resource target)
        {
            _resource = target;
            _mover.SetTarget(target.transform);
            IsFree = false;
        }

        private void SetTarget(Transform target)
        {
            _mover.SetTarget(target);
            IsFree = false;
        }

        public void ChangeModeToCreatingBase(GameObject flag)
        {
            BotsGarage garage = _home.GetComponent<BotsGarage>();
            
            garage.RemoveBot(GetComponent<CollectingBot>());
            
            SetTarget(flag.transform);

            if (TryGetComponent(out _builder) == false)
            {
                _builder = gameObject.AddComponent<BaseBuilder>();
            }
            else
            {
                _builder.enabled = true;
            }

            _builder.SetFlag(flag);
            
            _mover.OnTargetReached -= GiveResource;
            _builder.OnBaseBuilt += ChangeModeToCollectingResource;
        }
        
        private void ChangeModeToCollectingResource()
        {
            if (_builder != null)
            {
                _builder.enabled = false;
                _builder.OnBaseBuilt -= ChangeModeToCollectingResource;
                _mover.OnTargetReached += GiveResource;
            }
        }

        private void GiveResource()
        {
            _base.PickUpResource(_resource);
            SetFree();
        }

        private void SetFree()
        {
            _mover.Stop();
            _resource = null;
            IsFree = true;
        }

        private void GoHome()
        {
            _mover.SetTarget(_home);
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