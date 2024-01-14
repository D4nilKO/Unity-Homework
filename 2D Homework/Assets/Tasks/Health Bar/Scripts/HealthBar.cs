using Tasks.Platformer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Tasks.Health_Bar.Scripts
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Health _health;

        protected Slider Bar;

        private void Awake()
        {
            Bar = gameObject.GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _health.HealthChanged += UpdateBar;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateBar;

            OnDisableAdditional();
        }

        protected virtual void UpdateBar(float health)
        {
            Bar.value = health / _health.MaxHealth;
        }
        
        protected virtual void OnDisableAdditional()
        {
        }
    }
}