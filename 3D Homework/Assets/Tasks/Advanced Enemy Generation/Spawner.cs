using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks.Advanced_Enemy_Generation
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] [Min(1)] private float _timeToSpawn = 1f;
        [SerializeField] private List<SpawnPoint> _spawnPoints;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            WaitForSeconds waitSpawn = new(_timeToSpawn);

            while (true)
            {
                yield return waitSpawn;

                SpawnPoint point = GetRandomPoint();

                MoverToTarget moverToTarget = Instantiate(point.GetEnemy(), point.transform.position, Quaternion.identity);
                moverToTarget.SetTarget(point.GetEnemyTarget());
            }
        }

        private SpawnPoint GetRandomPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        }
    }
}