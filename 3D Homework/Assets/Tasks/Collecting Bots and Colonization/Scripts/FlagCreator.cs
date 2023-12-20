using System.Collections;
using UnityEngine;

namespace Tasks.Collecting_Bots_and_Colonization.Scripts
{
    public class FlagCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _flagPrefab;
        
        private float _groundHeight;

        private bool _isPlacingFlag;

        public GameObject Flag { get; private set; }
        private Coroutine _currentSettingFlag;

        private Camera _mainCamera;

        public bool IsFlagSet;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _groundHeight = transform.localPosition.y;
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

            Vector3 newFlagPosition = GetGlobalPointOnPlane();

            if (IsFlagSet == false)
            {
                Flag = Instantiate(_flagPrefab, newFlagPosition, Quaternion.identity);
                IsFlagSet = true;
            }
            else
            {
                Flag.SetActive(true);
            }

            while (_isPlacingFlag)
            {
                yield return null;

                newFlagPosition = GetGlobalPointOnPlane();
                Flag.transform.position = newFlagPosition;

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

        private Vector3 GetGlobalPointOnPlane()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            return Physics.Raycast(ray, out RaycastHit placeInfo)
                ? new Vector3(placeInfo.point.x, _groundHeight, placeInfo.point.z)
                : Vector3.zero;
        }
    }
}