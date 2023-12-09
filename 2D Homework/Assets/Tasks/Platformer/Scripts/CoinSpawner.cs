using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float _timeToSpawn = 1f;
        [SerializeField] private Coin _coin;
        [SerializeField] private List<Transform> _spawnPoints = new();

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            WaitForSeconds secondsToSpawn = new(_timeToSpawn);

            foreach (Transform point in _spawnPoints)
            {
                yield return secondsToSpawn;

                Instantiate(_coin, point.position, Quaternion.identity);
            }
        }
    }
}