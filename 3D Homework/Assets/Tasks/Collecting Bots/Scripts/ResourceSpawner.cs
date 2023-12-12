using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tasks.Collecting_Bots.Scripts
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private Resource _resource;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _spawnRadius;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            WaitForSeconds waitSpawn = new(_spawnDelay);

            while (true)
            {
                Instantiate(_resource, GetRandomPointForSpawn(), Quaternion.identity, transform);
                yield return waitSpawn;
            }
        }

        private Vector3 GetRandomPointForSpawn()
        {
            return VectorAddition(transform.position, Random.insideUnitCircle * _spawnRadius);
        }

        private static Vector3 VectorAddition(Vector3 vector3, Vector2 vector2)
        {
            return new Vector3(vector3.x + vector2.x, vector3.y, vector3.z + vector2.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}