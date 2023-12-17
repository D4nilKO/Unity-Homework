using System;
using System.Collections;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class FlagCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _flagPrefab;
        [SerializeField] private GameObject _levelGround;

        private bool _isPlacingFlag;

        public GameObject Flag { get; private set; }
        private Coroutine _currentSettingFlag;

        private Camera _mainCamera;
        
        public event Action OnFlagPlaced;

        private bool _isFlagSet;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            StartSettingFlag();
        }

        private void StartSettingFlag()
        {
            StopCurrentWork();
            _currentSettingFlag = StartCoroutine(SettingFlag());
        }

        private IEnumerator SettingFlag()
        {
            _isPlacingFlag = true;

            if (_isFlagSet == false)
            {
                Flag = Instantiate(_flagPrefab, GetMousePoint(), Quaternion.identity);
                
                _isFlagSet = true;
                OnFlagPlaced?.Invoke();
            }

            while (_isPlacingFlag)
            {
                yield return null;

                Vector3 position = GetMousePoint();
                Flag.transform.position = position;

                if (Input.GetMouseButtonDown(0))
                {
                    _isPlacingFlag = false;
                }
            }
        }

        private void StopCurrentWork()
        {
            if (_currentSettingFlag != null)
            {
                StopCoroutine(_currentSettingFlag);
            }
        }

        private Vector3 GetMousePoint()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit placeInfo))
            {
                return placeInfo.point;
            }

            return Vector3.zero;
        }
    }
}