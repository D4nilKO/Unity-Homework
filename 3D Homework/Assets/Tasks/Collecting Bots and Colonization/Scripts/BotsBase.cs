using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    [RequireComponent(typeof(BaseScanner))]
    [RequireComponent(typeof(BotsGarage))]
    [RequireComponent(typeof(BaseWallet))]
    public class BotsBase : MonoBehaviour
    {
        [SerializeField] private Transform _resourcesParent;

        [SerializeField] [Min(0.1f)] private float _timeBetweenBaseTicks = 0.5f;

        [SerializeField] private int _botCoast = 3;
        [SerializeField] private int _baseCoast = 6;

        [SerializeField] private ResourceMaterial _resourceToCreateBot = ResourceMaterial.Iron;
        [SerializeField] private ResourceMaterial _resourceToCreateBase = ResourceMaterial.Iron;

        private bool _isBaseBuilt = false;

        private List<Resource> _freeResources = new();
        private List<Resource> _busyResources = new();

        private Coroutine _currentWork;

        private BaseScanner _scanner;
        private BotsGarage _garage;
        private BaseWallet _wallet;
        private FlagCreator _flagCreator;

        private void Start()
        {
            _scanner = GetComponent<BaseScanner>();
            _garage = GetComponent<BotsGarage>();
            _wallet = GetComponent<BaseWallet>();
            _flagCreator = GetComponent<FlagCreator>();

            _currentWork = StartCoroutine(Work());
        }

        private IEnumerator Work()
        {
            WaitForSeconds waitTime = new(_timeBetweenBaseTicks);

            while (true)
            {
                AddFreeResources();

                if (_isBaseBuilt == false && _flagCreator.IsFlagSet)
                {
                    TryCreateBase(_flagCreator.Flag);
                }
                else
                {
                    TryCreateBot();
                }

                SetWorkForAllBots();

                yield return waitTime;
            }
        }

        private void TryCreateBase(GameObject flag)
        {
            if (_garage.FreeBotsCount > 0)
            {
                if (_wallet.TrySpendResourceValue(_resourceToCreateBase, _baseCoast))
                {
                    _garage.TryGetFreeBot(out CollectingBot freeBot);

                    freeBot.ChangeModeToCreatingBase(flag);
                    _isBaseBuilt = true;
                }
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_currentWork);
        }

        public void PickUpResource(Resource resource)
        {
            _wallet.AddResourceValue(resource);

            resource.transform.parent = _resourcesParent;
            resource.gameObject.SetActive(false);
        }

        private bool CheckAvailabilityInLists(Resource resource)
        {
            return _freeResources.Contains(resource) || _busyResources.Contains(resource);
        }

        private void TryCreateBot()
        {
            if (_wallet.TrySpendResourceValue(_resourceToCreateBot, _botCoast))
            {
                _garage.CreateBot();
            }
        }

        private void AddFreeResources()
        {
            List<Resource> resources = _scanner.FindResourcesInCollectionArea();

            foreach (Resource resource in resources.Where(resource => CheckAvailabilityInLists(resource) == false))
            {
                _freeResources.Add(resource);
            }
        }

        private void SetWorkForAllBots()
        {
            int freeBotsCount = _garage.FreeBotsCount;

            for (int i = 0; i < freeBotsCount; i++)
            {
                SetWorkForBot();
            }
        }

        private void SetWorkForBot()
        {
            if (_garage.TryGetFreeBot(out CollectingBot freeBot) && TryGetFreeResource(out Resource freeResource))
            {
                freeBot.SetTarget(freeResource);
                _busyResources.Add(freeResource);
                _freeResources.Remove(freeResource);
            }
        }

        private bool TryGetFreeResource(out Resource resource)
        {
            resource = _freeResources.FirstOrDefault();

            return resource != null;
        }
    }
}