using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(ObjectLocator))]
    [RequireComponent(typeof(MoverToTarget))]
    [RequireComponent(typeof(MoverByPoints2D))]
    public class EnemyMoverSwitcher : MonoBehaviour
    {
        private ObjectLocator _objectLocator;
        private MoverToTarget _moverToTarget;
        private MoverByPoints2D _moverByPoints;

        private Player _player;

        private void Awake()
        {
            _objectLocator = GetComponent<ObjectLocator>();
            _moverToTarget = GetComponent<MoverToTarget>();
            _moverByPoints = GetComponent<MoverByPoints2D>();
        }

        private void Update()
        {
            _objectLocator.TrackingLocate();
        }

        private void OnEnable()
        {
            _objectLocator.TargetFounded += SetTarget;
            _objectLocator.TargetLost += GoToPoints;
        }

        private void OnDisable()
        {
            _objectLocator.TargetFounded -= SetTarget;
            _objectLocator.TargetLost -= GoToPoints;
        }

        private void SetTarget(GameObject player)
        {
            if (player.TryGetComponent(out Player playerComponent))
            {
                _player = playerComponent;
                GoToPlayer();
            }
            else
            {
                Debug.LogWarning("Player component not found in target");
            }
        }

        private void GoToPlayer()
        {
            _moverToTarget.SetTarget(_player.transform);
            _moverByPoints.enabled = false;
        }

        private void GoToPoints()
        {
            _moverByPoints.enabled = true;
            _moverByPoints.UpdateTarget();
        }
    }
}