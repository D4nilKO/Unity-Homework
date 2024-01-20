using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float _startMaxHealth = 1f;

        private float _currentHealth;

        public event Action DamageApplied;
        public event Action<float> HealthChanged;

        public float MaxHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        public float CurrentHealth
        {
            get => _currentHealth;

            private set
            {
                _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
                
                HealthChanged?.Invoke(_currentHealth);

                if (IsAlive == false)
                    Dead();
            }
        }

        private void Awake()
        {
            MaxHealth = _startMaxHealth;
            UpdateHealthToMax();
        }

        public void Heal(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            CurrentHealth += value;
        }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
                return;

            CurrentHealth -= damage;

            DamageApplied?.Invoke();
        }

        public void SetMaxHealth(float maxHealth)
        {
            if (maxHealth <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxHealth));

            MaxHealth = maxHealth;

            UpdateHealthToMax();
        }

        private void UpdateHealthToMax()
        {
            CurrentHealth = MaxHealth;
        }

        private void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}