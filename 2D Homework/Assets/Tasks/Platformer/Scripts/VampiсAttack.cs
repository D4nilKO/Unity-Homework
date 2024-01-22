using System.Collections;
using UnityEngine;

namespace Tasks.Platformer.Scripts
{
    [RequireComponent(typeof(ObjectLocator))]
    [RequireComponent(typeof(Health))]
    public class VampiсAttack : MonoBehaviour
    {
        [SerializeField] [Min(0.1f)] private float _tickDamage = 0.1f;
        [SerializeField] [Min(0.1f)] private float _timeToTick = 0.1f;
        [SerializeField] [Min(0.1f)] private float _duration = 6f;

        [SerializeField] private KeyCode _keyCode = KeyCode.E;

        private Coroutine _currentCoroutine;
        private bool _isActive;

        private ObjectLocator _objectLocator;
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _objectLocator = GetComponent<ObjectLocator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_keyCode) && _isActive == false)
            {
                StartDamaging();
            }
        }

        private void OnDisable()
        {
            StopDamaging();
        }

        private void StartDamaging()
        {
            StopDamaging();
            _currentCoroutine = StartCoroutine(StealingHealth());
        }

        private void StopDamaging()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _isActive = false;
            }
        }

        private IEnumerator StealingHealth()
        {
            WaitForSeconds tick = new(_timeToTick);
            int countOfTicks = Mathf.CeilToInt(_duration / _timeToTick);

            _isActive = true;

            for (int i = 0; i < countOfTicks; i++)
            {
                if (_objectLocator.TryLocate(out GameObject target))
                {
                    StealHealth(target, _tickDamage);
                }

                yield return tick;
            }

            _isActive = false;
        }

        private void StealHealth(GameObject target, float health)
        {
            if (target.activeInHierarchy && target.TryGetComponent(out Health targetHealth) && targetHealth.IsAlive)
            {
                targetHealth.ApplyDamage(health);
                _health.Heal(health);
            }
        }
    }
}