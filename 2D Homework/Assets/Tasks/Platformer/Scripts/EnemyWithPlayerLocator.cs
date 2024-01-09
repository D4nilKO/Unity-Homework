﻿using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(PlayerLocator))]
    [RequireComponent(typeof(MoverToTarget))]
    [RequireComponent(typeof(MoverByPoints2D))]
    public class EnemyWithPlayerLocator : Enemy
    {
        private PlayerLocator _playerLocator;
        private MoverToTarget _moverToTarget;
        private MoverByPoints2D _moverByPoints;

        private Player _player;

        private void Awake()
        {
            _playerLocator = GetComponent<PlayerLocator>();
            _moverToTarget = GetComponent<MoverToTarget>();
            _moverByPoints = GetComponent<MoverByPoints2D>();
        }

        private void OnEnable()
        {
            _playerLocator.OnPlayerLocated += SetPlayer;
            _playerLocator.OnPlayerMissing += GoToPoints;
        }

        private void OnDisable()
        {
            _playerLocator.OnPlayerLocated -= SetPlayer;
            _playerLocator.OnPlayerMissing -= GoToPoints;
        }

        private void SetPlayer(Player player)
        {
            _player = player;

            GoToPlayer();
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