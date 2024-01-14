using Tasks.Platformer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Tasks.Health_Bar
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Health _health;
        
        protected Slider Bar;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            _health.HealthChanged += UpdateBar;
            
            OnEnableAdditional();
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateBar;
            
            OnDisableAdditional();
        }

        protected virtual void OnEnableAdditional()
        {
        }

        protected virtual void OnDisableAdditional()
        {
        }

        private void Init()
        {
            Bar = gameObject.GetComponent<Slider>();
        }

        protected virtual void UpdateBar(float health)
        {
            FillBar(health);
        }

        protected void FillBar(float health)
        {
            Bar.value = health / _health.MaxHealth;
        }
    }
}