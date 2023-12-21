using System;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CollectingBot))]
    [RequireComponent(typeof(MoverToTarget))]
    public class BaseBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject _basePrefab;

        private CollectingBot _collectingBot;
        private MoverToTarget _moverToTarget;

        [SerializeField]private GameObject _currentFlag;
        [SerializeField]private BotsBase _currentBase;
        
        private ResourcesDatabase _resourcesDatabase;

        public event Action OnBaseBuilt;

        private void Awake()
        {
            _collectingBot = GetComponent<CollectingBot>();
            _moverToTarget = GetComponent<MoverToTarget>();
            
            _basePrefab = _collectingBot.GetBasePrefab;
        }

        private void OnEnable()
        {
            _moverToTarget.OnTargetReached += BuildBase;
        }

        private void OnDisable()
        {
            _moverToTarget.OnTargetReached -= BuildBase;
        }
        
        private void Init( ResourcesDatabase resourcesDatabase)
        {
            _resourcesDatabase = resourcesDatabase;
        }
        
        public void SetFlag(GameObject flag)
        {
            _currentFlag = flag;
        }

        private void BuildBase()
        {
             _currentBase = Instantiate(_basePrefab, _currentFlag.transform.position, Quaternion.identity).GetComponentInChildren<BotsBase>();
            ConfigureBase(_currentBase, _resourcesDatabase);
            
            _currentFlag.SetActive(false);
            
            OnBaseBuilt?.Invoke();
        }

        private void ConfigureBase(BotsBase botsBase, ResourcesDatabase resourcesDatabase)
        {
            transform.parent = botsBase.transform;

            botsBase.Init(resourcesDatabase);

            botsBase.GetComponent<BotsGarage>().AddBot(_collectingBot);

            _collectingBot.Initialize();
        }
    }
}