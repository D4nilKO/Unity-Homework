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

        public void SetFlag(GameObject flag)
        {
            _currentFlag = flag;
        }

        public void BuildBase()
        {
             _currentBase = Instantiate(_basePrefab, _currentFlag.transform.position, Quaternion.identity).GetComponentInChildren<BotsBase>();
            ConfigureBase(_currentBase);
            
            Destroy(_currentFlag);
            
            OnBaseBuilt?.Invoke();
        }

        private void ConfigureBase(BotsBase botsBase)
        {
            transform.parent = botsBase.transform;

            botsBase.GetComponent<BotsGarage>().AddBot(_collectingBot);

            _collectingBot.Initialize();
        }
    }
}