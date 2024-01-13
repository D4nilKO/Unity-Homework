using System;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float _startMaxHealth = 1f;

        private float _currentHealth;

        public event Action OnDamageApplied;

        public float MaxHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        public float CurrentHealth
        {
            get => _currentHealth;

            private set
            {
                _currentHealth = Mathf.Clamp(value, 0f, MaxHealth);
                Debug.Log($"{gameObject.name} - current Health = {_currentHealth}");

                if (_currentHealth <= 0)
                    gameObject.SetActive(false);
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

            OnDamageApplied?.Invoke();
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
    }
}