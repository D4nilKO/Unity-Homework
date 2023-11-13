using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Enemy_Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] [Min(1)] private float _timeToSpawn = 1f;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private List<Transform> _spawnPoints = new();
        
        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            float time = 0;

            while (time <= _timeToSpawn)
            {
                time += Time.deltaTime;
                yield return null;
            }

            Enemy enemy = Instantiate(_enemy, GetRandomPoint().position, Quaternion.identity);
            enemy.Init(GetRandomDirection());

            StartCoroutine(Spawn());
        }

        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized; // ?
        }

        private Transform GetRandomPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        }
    }
}