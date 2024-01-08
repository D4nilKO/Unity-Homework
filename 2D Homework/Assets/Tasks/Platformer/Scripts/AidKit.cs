using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class AidKit : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float _healthRestoreAmount = 1f;

        public float HealthRestoreAmount => _healthRestoreAmount;

        public void RestoreHealth(GameObject healObject)
        {
            if (healObject.TryGetComponent(out Health health))
            {
                health.Heal(_healthRestoreAmount);
                Debug.Log("Health restored");
            }
        }
    }
}