using UnityEngine;

namespace Tasks.Advanced_Enemy_Generation
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private MoverToTarget _enemy;
        [SerializeField] private Transform _enemyTarget;

        public MoverToTarget GetEnemy()
        {
            return _enemy;
        }

        public Transform GetEnemyTarget()
        {
            return _enemyTarget;
        }
    }
}